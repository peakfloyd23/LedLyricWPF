using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LedLyricWPF
{
    /// <summary>
    /// LyricFontSetting.xaml 的交互逻辑
    /// </summary>
    public partial class LyricFontSetting : Window
    {

        private LyricSettingConfig config = new LyricSettingConfig();
        public LyricFontSetting()
        {
            InitializeComponent();
            initSetting();
        }

        private void initSetting()
        {
            config = LyricSettingConfig.loadConfig("config.json");
            XPosition.Text=config.XPosition.ToString();
            YPosition.Text=config.YPosition.ToString();
            Width.Text=config.Width.ToString();
            Height.Text=config.Height.ToString();
            FontName.Content = config.Name;
            Bold.IsChecked = config.Bold;
            Italics.IsChecked = config.Italic;
            FontSize.Content = config.Size;
            FontColor.Content = config.ColorName;
        }


        private void selectFont_Click(object sender, RoutedEventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.ShowColor = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.Font != null)
                {
                    config.Color = dlg.Color;
                    config.ColorName = dlg.Color.Name;
                    config.Bold = dlg.Font.Bold;
                    config.Italic = dlg.Font.Italic;
                    config.Underline = dlg.Font.Underline;
                    config.FontHeight=dlg.Font.Height;
                    config.Name = dlg.Font.Name;
                    config.SizeInPoints = dlg.Font.SizeInPoints;
                    config.Strikeout = dlg.Font.Strikeout;
                    config.Unit = dlg.Font.Unit;
                    config.SystemFontName = dlg.Font.SystemFontName;
                    config.Size = dlg.Font.Size;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            config.XPosition = int.Parse(XPosition.Text);
            config.YPosition = int.Parse(YPosition.Text);
            config.Width=int.Parse(Width.Text);
            config.Height=int.Parse(Height.Text);
            try
            {
                LyricSettingConfig.saveSetting(config);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            short val;
            if (!Int16.TryParse(e.Text, out val))
                e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }


}
