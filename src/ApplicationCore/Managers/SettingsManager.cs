using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public abstract class BaseSettingsManager
    {
        private Configuration _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public BaseSettingsManager()
        {
            _isDevelopment = GetSettingValue("Environment") == "Development";
        }

        bool _isDevelopment = true;
        public bool IsDevelopment => _isDevelopment;

        KeyValueConfigurationCollection Settings => _configuration.AppSettings.Settings;
        public string GetSettingValue(string key) => Settings[key].Value;

        public string AddUpdateAppSettings(string key, string value)
        {
            try
            {
                if (Settings[key] == null) Settings.Add(key, value);
                else Settings[key].Value = value;

                _configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(_configuration.AppSettings.SectionInformation.Name);

                return "";
            }
            catch (ConfigurationErrorsException)
            {
                return "寫入設定檔失敗";
            }
        }

    }
}
