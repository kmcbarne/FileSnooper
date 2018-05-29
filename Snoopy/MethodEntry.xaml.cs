using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MethodEntry.xaml
    /// </summary>
    public partial class MethodEntry : UserControl
    {
        public MethodEntry()
        {
            InitializeComponent();
        }

        private static DependencyProperty MethodNameProperty = DependencyProperty.Register("MethodName", typeof(string),
            typeof(MethodEntry), new PropertyMetadata("[No Method Name Found]"));

        private static DependencyProperty MemberInfoProperty = DependencyProperty.Register("MemberInfo", typeof(string),
            typeof(MethodEntry), new PropertyMetadata("Public"));

        private static DependencyProperty ReturnTypeProperty = DependencyProperty.Register("ReturnType", typeof(string),
            typeof(MethodEntry), new PropertyMetadata("[No Return Type Found]"));

        //Potentially switch to string array eventually
        private static DependencyProperty ArgumentsProperty = DependencyProperty.Register("Arguments", typeof(string),
            typeof(MethodEntry), new PropertyMetadata("[No Arguments Found]"));

        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set
            {
                SetValue(MethodNameProperty, value);
                methodName.Text = value;
            }
        }

        public string MemberInfo
        {
            get { return (string)GetValue(MemberInfoProperty); }
            set
            {
                switch(value)
                {
                    case "Public":
                        pubInfo.IsChecked = true;
                        protInfo.IsChecked = false;
                        intInfo.IsChecked = false;
                        privInfo.IsChecked = false;
                        break;
                    case "Protected":
                        pubInfo.IsChecked = false;
                        protInfo.IsChecked = true;
                        intInfo.IsChecked = false;
                        privInfo.IsChecked = false;
                        break;
                    case "Internal":
                        pubInfo.IsChecked = false;
                        protInfo.IsChecked = false;
                        intInfo.IsChecked = true;
                        privInfo.IsChecked = false;
                        break;
                    case "Private":
                        pubInfo.IsChecked = false;
                        protInfo.IsChecked = false;
                        intInfo.IsChecked = false;
                        privInfo.IsChecked = true;
                        break;
                }
                SetValue(MemberInfoProperty, value);
            }
        }

        public string ReturnType
        {
            get { return (string)GetValue(ReturnTypeProperty); }
            set
            {
                SetValue(ReturnTypeProperty, value);
                returnType.Text = value;
            }
        }

        public string Arguments
        {
            get { return (string)GetValue(ArgumentsProperty); }
            set
            {
                SetValue(ArgumentsProperty, value);
                arguments.Text = value;
            }
        }

        public IEnumerable<Border> Children
        {
            get
            {
                IEnumerable<Border> children = new Border[] { columnA, columnB, columnC, columnD };
                return children;
            }            
        }
    }
}
