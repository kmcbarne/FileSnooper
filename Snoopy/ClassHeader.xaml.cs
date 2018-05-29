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
    /// Interaction logic for ClassHeader.xaml
    /// </summary>
    public partial class ClassHeader : UserControl
    {
        public ClassHeader()
        {
            InitializeComponent();
        }

        private static DependencyProperty ClassNameProperty = DependencyProperty.Register("ClassName", typeof(string),
            typeof(ClassHeader), new PropertyMetadata(""));
        private static DependencyProperty NameSpaceProperty = DependencyProperty.Register("NameSpace", typeof(string),
            typeof(ClassHeader), new PropertyMetadata(""));
        private static DependencyProperty FullNameProperty = DependencyProperty.Register("FullName", typeof(string),
            typeof(ClassHeader), new PropertyMetadata(""));

        public string ClassName
        {
            get { return (string)GetValue(ClassNameProperty); }
            set
            {
                className.Text = value;
                SetValue(ClassNameProperty, value);
            }
        }

        public string NameSpace
        {
            get { return (string)GetValue(NameSpaceProperty); }
            set
            {
                nameSpace.Text = value;
                SetValue(ClassNameProperty, value);
            }
        }

        public string FullName
        {
            get { return (string)GetValue(FullNameProperty); }
            set
            {
                fullName.Text = value;
                SetValue(FullNameProperty, value);
            }
        }
    }
}
