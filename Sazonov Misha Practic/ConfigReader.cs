using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sazonov_Misha_Practic
{
    public class ConfigReader
    {
        private readonly string configFile;
        private readonly Dictionary<string, Dictionary<string, string>> configData;

        public ConfigReader(string configFile)
        {
            this.configFile = configFile;
            configData = new Dictionary<string, Dictionary<string, string>>();
            LoadConfig();
        }

        private void LoadConfig()
        {
            if (!File.Exists(configFile))
            {
                // Обработка ситуации, когда файл конфигурации отсутствует
                return;
            }

            string currentSection = null;

            foreach (string line in File.ReadLines(configFile))
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    configData[currentSection] = new Dictionary<string, string>();
                }
                else if (currentSection != null && trimmedLine.Contains("="))
                {
                    string[] parts = trimmedLine.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    configData[currentSection][key] = value;
                }
            }
        }

        public string GetValue(string section, string key)
        {
            if (configData.ContainsKey(section) && configData[section].ContainsKey(key))
            {
                return configData[section][key];
            }

            return null;
        }
    }
}
