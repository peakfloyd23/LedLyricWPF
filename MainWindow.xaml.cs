using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps.Packaging;

namespace LedLyricWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string CurrentLyricFile = string.Empty;
        private string CurrentLyricFileExtension = string.Empty;
        private int SelectedIndex = -1;
        private Dictionary<string, string> lyricFileMap = new Dictionary<string, string>();
        private LineLyricText lineLyricText = new LineLyricText();
        private LyricSettingConfig settingConfig;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void initLyricSetting()
        {
            this.settingConfig = LyricSettingConfig.loadConfig("config.json");
        }



        private void lyricText_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void lyricFiles_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void Load_Lyric_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件 (*.txt)|*.txt|word文档 (*.doc,*.docx)|*.doc;*.docx";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                CurrentLyricFile = openFileDialog.FileName;
                CurrentLyricFileExtension = System.IO.Path.GetExtension(CurrentLyricFile);
                var fileName = System.IO.Path.GetFileName(CurrentLyricFile);

                switch (CurrentLyricFileExtension)
                {
                    case ".doc":
                        fileContent = this.readWord(CurrentLyricFile);
                        break;
                    case ".docx":
                        fileContent = this.readWord(CurrentLyricFile);
                        break;
                    case ".txt":
                        fileContent = this.readText(openFileDialog);
                        break;
                }
                this.lyricText.Text = fileContent;
                this.lyricFileMap.Add(fileName, CurrentLyricFile);
                this.lyricFiles.Items.Add(fileName);
            }
        }

        private string readWord(string word)
        {
            var sb = new StringBuilder();
            var fs = new FileStream(this.CurrentLyricFile, FileMode.Open, FileAccess.Read);
            XWPFDocument document = new XWPFDocument(fs);
            foreach (var para in document.Paragraphs)
            {
                sb.AppendLine(para.ParagraphText);
            }
            document.Close();
            fs.Close();
            fs.Dispose();
            return sb.ToString();
        }


        /**读取txt字幕，返回字符串*/
        private string readText(OpenFileDialog openFileDialog)
        {
            var fileStream = openFileDialog.OpenFile();
            StreamReader sr = new StreamReader(fileStream);
            var text = sr.ReadToEnd();
            sr.Close();
            fileStream.Close();
            return text;

        }

        /**读取txt字幕，返回字符串*/
        private string readText(string file)
        {
            var fileStream = new FileStream(file, FileMode.Open);
            StreamReader sr = new StreamReader(fileStream);
            var text = sr.ReadToEnd();
            sr.Close();
            fileStream.Close();
            return text;

        }


        private void Load_Setting_Click(object sender, RoutedEventArgs e)
        {
            LyricFontSetting setting = new LyricFontSetting();
            setting.ShowDialog();
        }



        private void writeTxtLyric()
        {
            FileStream fileStream = new FileStream(this.CurrentLyricFile, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteAsync(this.lyricText.Text);
            writer.Flush();
            fileStream.Close();
        }

        private void writeWordLyric()
        {
            FileStream fileStream = new FileStream(this.CurrentLyricFile, FileMode.OpenOrCreate, FileAccess.Write);
            XWPFDocument document = new XWPFDocument();
            for (var line = 0; line < lyricText.LineCount; line++)
            {
                XWPFParagraph para = document.CreateParagraph();
                XWPFRun run = para.CreateRun();
                run.SetText(lyricText.GetLineText(line));
            }
            document.Write(fileStream);
            document.Close();
            fileStream.Close();
        }

        private void Show_Lyric_Click(object sender, RoutedEventArgs e)
        {
            if (lineLyricText == null)
            {
                lineLyricText = new LineLyricText();
            }
            if (settingConfig != null)
            {
                lineLyricText.WindowStartupLocation = WindowStartupLocation.Manual;
                lineLyricText.Left = settingConfig.XPosition;
                lineLyricText.Top = settingConfig.YPosition;
                lineLyricText.Width = settingConfig.Width;
                lineLyricText.Height = settingConfig.Height;

                lineLyricText.FontFamily = new System.Windows.Media.FontFamily(settingConfig.Name);
                lineLyricText.FontSize = settingConfig.Size;
                if (settingConfig.Bold)
                {
                    lineLyricText.FontWeight = FontWeights.Bold;
                }
                if (settingConfig.Italic)
                {
                    lineLyricText.FontStyle = FontStyles.Italic;
                }

                System.Windows.Media.SolidColorBrush scBrush = new System.Windows.Media.SolidColorBrush();
                System.Windows.Media.Color clr = new System.Windows.Media.Color();

                clr.A = settingConfig.Color.A;
                clr.B = settingConfig.Color.B;
                clr.G = settingConfig.Color.G;
                clr.R = settingConfig.Color.R;
                scBrush.Color = clr;
                lineLyricText.Foreground = scBrush;
            }
            if(lyricText.LineCount > 0)
            {
                lineLyricText.setLineTextBoxValue(lyricText.GetLineText(0));
            }
            lineLyricText.Show();

        }

        private void Close_Lyric_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Lyric_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
