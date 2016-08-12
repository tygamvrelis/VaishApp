using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Vaish
{
    public class Load
    {

        public static ObservableCollection<string> TimerNames = new ObservableCollection<string>();
        public static Dictionary<string, DateTime> Timers = new Dictionary<string, DateTime>();

        public Load()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\" + "MySavedTimers.txt";
            if (File.Exists(path))
            {
                LoadSaves(path);
            }
        }

        private static void LoadSaves(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] words;
                    words = line.Split('\t');

                    if (words.Length < 2) { MessageBox.Show("Load failed due to a missing timer name or date"); break; }

                    string Name = words[0];
                    TimerNames.Add(Name);

                    string Date = words[1];
                    string[] date = Date.Split('-');

                    int year, month, day;
                    Int32.TryParse(date[0], out year);
                    Int32.TryParse(date[1], out month);
                    Int32.TryParse(date[2], out day);

                    Timers.Add(Name, new DateTime(year, month, day));
                }
            }
        }
    }
}
    
