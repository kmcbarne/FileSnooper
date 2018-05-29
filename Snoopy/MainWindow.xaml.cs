using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snoopy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string results = "";

        public MainWindow()
        {
            InitializeComponent();


            //Begin testing
            //ResultsInterface inter = new Snoopy.ResultsInterface();
            //MethodEntry info = new MethodEntry();
            //info.ReturnType = "System.Int32";
            //info.MethodName = "TestMethod()";
            //info.MemberInfo = "Private";
            //info.Arguments = "var";
            //inter.resultsPanel.Children.Add(info);
            //inter.Show();
            //End testing
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Title = "Select File";
            open.ShowDialog();

            string path = open.FileName;
            fileName.Text = path;
            ReflectFile(path);

            //Results winR = new Results();
            //winR.results.Text = ReflectFileSimple(path);
            //winR.Show();
        }


        /// <summary>
        /// Returns File Reflection information in a simple string format.
        /// </summary>
        /// <param name="path">Full pathname of the file to be reflected.</param>
        /// <returns>Formatted string member.</returns>
        public string ReflectFileSimple(string path)
        {
            var assembly = Assembly.LoadFile(path);
            int classNumber = 1;


            foreach(var type in assembly.GetTypes())
            {
                int methodNumber = 1;

                results += classNumber + $".  Class: {type.Name}:" + Environment.NewLine;
                results += $"    Namespace: {type.Namespace}" + Environment.NewLine;
                results += $"    Full name: {type.FullName}" + Environment.NewLine;

                results += $"    Methods:" + Environment.NewLine;

                foreach(var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    results += "\t" + methodNumber + $".Method: {methodInfo.Name}";

                    if(methodInfo.IsPublic)
                        results += $"                Public" + Environment.NewLine;
                    if(methodInfo.IsFamily)
                        results += $"                Protected" + Environment.NewLine;
                    if(methodInfo.IsAssembly)
                        results += $"                Internal" + Environment.NewLine;
                    if(methodInfo.IsPrivate)
                        results += $"                Private" + Environment.NewLine;

                    results += $"\t   ReturnType: {methodInfo.ReturnType}" + Environment.NewLine;
                    results += $"\t   Arguments: {string.Join(", ", methodInfo.GetParameters().Select(x => x.ParameterType))}" + Environment.NewLine;
                    methodNumber++;
                }
                classNumber++;
            }
            return results;
        }

        /// <summary>
        /// Displays File Reflection information in a window containing a StackPanel with a MethodEntry control for each reflected method.
        /// </summary>
        /// <param name="path">Full pathname of the file to be reflected.</param>
        public void ReflectFile(string path)
        {
            ResultsInterface visualResults = new ResultsInterface();
            

            var assembly = Assembly.LoadFile(path);
            int classNumber = 1;

            foreach (var type in assembly.GetTypes())
            {
                ClassHeader header = new ClassHeader();

                header.ClassName = type.Name;
                header.NameSpace = type.Namespace;
                header.FullName = type.FullName;

                visualResults.resultsPanel.Children.Add(header);

                results += classNumber + $".  Class: {type.Name}:" + Environment.NewLine;
                results += $"    Namespace: {type.Namespace}" + Environment.NewLine;
                results += $"    Full name: {type.FullName}" + Environment.NewLine;

                results += $"    Methods:" + Environment.NewLine;

                foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    MethodEntry temp = new MethodEntry();
                    temp.MethodName = methodInfo.Name;

                    if(methodInfo.IsPublic)
                        temp.MemberInfo = "Public";
                    if(methodInfo.IsFamily)
                        temp.MemberInfo = "Protected";
                    if(methodInfo.IsAssembly)
                        temp.MemberInfo = "Internal";
                    if(methodInfo.IsPrivate)
                        temp.MemberInfo = "Private";

                    if (verboseCheck.IsChecked == false)
                    {
                        temp.ReturnType = methodInfo.ReturnType.ToString();
                        temp.Arguments = string.Join(Environment.NewLine, methodInfo.GetParameters().Select(x => x.ParameterType));
                    }
                    else
                    {
                        temp.ReturnType = Truncate(methodInfo.ReturnType.ToString());

                        string arguments = "";
                        ParameterInfo[] argumentInfo = methodInfo.GetParameters();

                        foreach(ParameterInfo argument in argumentInfo)
                        {
                            arguments += Truncate(argument.ParameterType.ToString()) + Environment.NewLine;
                        }
                        temp.Arguments = arguments;
                    }
                    

                    //temp.Arguments = string.Join(", ", methodInfo.GetParameters().Select(x => x.ParameterType));
                    
                    visualResults.resultsPanel.Children.Add(temp);
                }
                classNumber++;
            }
            
            visualResults.Show();
            FixWidth(visualResults);
            FixHeight(visualResults);
            visualResults.UpdateLayout();
        }

        public string Truncate(string verboseString)
        {
            Regex w = new Regex(@".*?\.");

            return w.Replace(verboseString, "");
        }

        public void FixHeight(ResultsInterface visualResults)
        {
            double tallest = 0.0;

            foreach (Control child in visualResults.resultsPanel.Children)
            {
                if(child is MethodEntry)
                {
                    MethodEntry methodChild = (MethodEntry)child;

                    foreach(Border border in methodChild.Children)
                    {
                        if(border.ActualHeight > tallest)
                            tallest = border.ActualHeight;
                    }

                    foreach(Border border in methodChild.Children)
                        border.Height = tallest;
                }
            }
        }

        public void FixWidth(ResultsInterface visualResults)
        {
            double widestA = 0.0;
            double widestB = 0.0;
            double widestC = 0.0;
            double widestD = 0.0;

            foreach(Control child in visualResults.resultsPanel.Children)
            {
                if(child is MethodEntry)
                {
                    MethodEntry methodChild = (MethodEntry)child;

                    if(methodChild.columnA.ActualWidth > widestA)
                        widestA = methodChild.columnA.ActualWidth;

                    if(methodChild.columnB.ActualWidth > widestB)
                        widestB = methodChild.columnB.ActualWidth;

                    if(methodChild.columnC.ActualWidth > widestC)
                        widestC = methodChild.columnC.ActualWidth;

                    if(methodChild.columnD.ActualWidth > widestD)
                        widestD = methodChild.columnD.ActualWidth;
                }
            }

            foreach(Control child in visualResults.resultsPanel.Children)
            {
                if(child is MethodEntry)
                {
                    MethodEntry methodChild = (MethodEntry)child;

                    methodChild.columnA.Width = widestA;
                    methodChild.columnB.Width = widestB;
                    methodChild.columnC.Width = widestC;
                    methodChild.columnD.Width = widestD;
                }
            }
        }

        private void fileName_Drop(object sender, DragEventArgs e)
        {
            
        }
    }
}
