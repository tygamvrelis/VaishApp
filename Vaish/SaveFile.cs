using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Vaish
{
    public static class SaveFile
    {
        public static string SaveBoxContent { get; set; }

        public static void Save(DateTime date)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string filename = SaveBoxContent;
            path = path + "\\" + "MySavedTimers.txt";

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, true))
            {
                file.WriteLine("{0}\t{1}", SaveBoxContent, date.ToShortDateString());
            }

            MessageBox.Show("Saved to " + path);
        }
    }
}
