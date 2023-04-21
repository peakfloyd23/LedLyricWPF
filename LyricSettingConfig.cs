using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LedLyricWPF
{
    class LyricSettingConfig
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public float Size { get; set; }
        public int Style { get; set; }  
        public bool Bold { get; set; }  
        public bool Italic { get; set; }
        public bool Strikeout { get; set; }
        public bool Underline { get; set; }
        public string Name { get; set; }
        public GraphicsUnit Unit { get; set; }
        public string SystemFontName { get; set; }
        public int FontHeight { get;set; }
        public float SizeInPoints { get; set; }

        public Color Color { get; set; }

        public string ColorName { get; set; }   





        public static void saveSetting(LyricSettingConfig config)
        {


            var resultJson = JsonSerializer.Serialize(config);
            try
            {
                File.WriteAllText("config.json", resultJson);
            }
            catch
            {

            }
        }

        public static LyricSettingConfig loadConfig(string file)
        {
            try
            {
                var jsonStr = File.ReadAllText(file);
                LyricSettingConfig? config = JsonSerializer.Deserialize<LyricSettingConfig>(jsonStr);
                return config;
            }
            catch
            {

            }
            return null;
        }

    }
}
