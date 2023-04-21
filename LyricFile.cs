using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LedLyricWPF
{
    class LyricFile
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public static void saveLyricFileList(List<LyricFile> lyricFiles)
        {
            var resultJson = JsonSerializer.Serialize(lyricFiles);
            try
            {
                File.WriteAllText("lyrics.json", resultJson);
            }
            catch
            {

            }
        }

        public static List<LyricFile> loadLyricFiles(string file)
        {
            try
            {
                var jsonStr = File.ReadAllText(file);
                List<LyricFile>? files = JsonSerializer.Deserialize<List<LyricFile>>(jsonStr);
                return files;
            }
            catch
            {

            }
            return null;
        }

    }
}
