using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace LedLyricWPF
{
    /// <summary>
    /// LineLyricText.xaml 的交互逻辑
    /// </summary>
    public partial class LineLyricText : Window
    {
        public LineLyricText()
        {
            InitializeComponent();
        }

        public void setLineTextBoxValue(string value)
        {
            LyricTextLabel.Content = value;
        }

    }
}
