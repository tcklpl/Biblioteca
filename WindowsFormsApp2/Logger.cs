using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp2 {
    class Logger {

        public static bool Log(String s) {
            try {
                String filename = DateTime.Now.ToString("ss_mm_HH__dd_MM_yyyy__LOG.txt");
                File.Create(filename);
                using (var tw = new StreamWriter(filename, true)) {
                    tw.Write(s);
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

    }
}
