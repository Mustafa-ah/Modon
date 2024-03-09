using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Maham.Helpers
{
    public static class Utility
    {
        public static bool NotSupportedExtension( string extension)
        {
            try
            {
                //https://developer.android.com/guide/topics/media/media-formats

                string extensions = 
                    ".avi"+
                    ".webmi"+
                    ".mjp"+
                    ".mjpg"+
                    ".mp4"+
                    ".mpg4"+
                    ".mp2"+
                    ".mpg2"+
                    ".mpg"+
                    ".3gp"+
                    ".3g2"+
                    ".m2ts"+
                    ".ota"+
                    ".mkv"+
                    ".AAC"+
                    ".MP3"+
                    ".AMR"+
                    ".Ogg"+
                    ".PCM"+
                    ".wav";

                return extensions.Contains(extension);
            }
            catch  
            {
                return false;
            }
        }


        internal static bool HasArabicCharacters(string text)
        {
            Regex regex = new Regex(
              "[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
            return regex.IsMatch(text);
        }
    }
}
