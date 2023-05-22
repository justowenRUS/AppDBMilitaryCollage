using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sazonov_Misha_Practic
{
    public class IniFile
    {
        private readonly Dictionary<string, Dictionary<string, string>> iniData;

        public IniFile(string filePath)
        {
            iniData = new Dictionary<string, Dictionary<string, string>>();
            string currentSection = null;

            foreach (string line in File.ReadLines(filePath))
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    iniData[currentSection] = new Dictionary<string, string>();
                }
                else if (currentSection != null && trimmedLine.Contains("="))
                {
                    string[] parts = trimmedLine.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    iniData[currentSection][key] = value;
                }
            }
        }

        public string GetValue(string section, string key)
        {
            if (iniData.ContainsKey(section) && iniData[section].ContainsKey(key))
            {
                return iniData[section][key];
            }

            return null;
        }
    }


    // Вспомогательный класс для импорта функций чтения и записи ini-файлов
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder value, int size, string filePath);

        [DllImport("kernel32.dll")]
        internal static extern bool WritePrivateProfileString(string section, string key, string value, string filePath);
    }
}