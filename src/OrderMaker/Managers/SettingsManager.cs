using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using ApplicationCore;
using ApplicationCore.Brokages;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Models;
using ApplicationCore.Security;
using Newtonsoft.Json;
using NLog;
using NLog.Targets;

namespace OrderMaker
{
    public interface ISettingsManager
    {
        BrokageSettings BrokageSettings { get; }
        string GetSettingValue(string key);
        string LogFilePath { get; }
        string SecurityKey { get; }


        List<TradeSettings> TradeSettings { get; }
        TradeSettings FindTradeSettings(string id);

        bool CheckBasicSetting();
        string AddUpdateAppSettings(string key, string value);

        void AddUpdateTradeSettings(TradeSettings tradeSettings);
        void RemoveTradeSettings(TradeSettings tradeSettings);

    }

    public class SettingsManager : BaseSettingsManager, ISettingsManager
    {
        string TradeSettingsFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings");
        string TradeSettingsPath => Path.Combine(TradeSettingsFolder, "trade.json");

        List<TradeSettings> _tradeSettings = new List<TradeSettings>();


        public SettingsManager()
        {
            _brokageSettings = new BrokageSettings
            {
                SID = GetSettingValue(AppSettingsKey.SID),
                Password = DecryptPassword(GetSettingValue(AppSettingsKey.Password)),
                IP = GetSettingValue(AppSettingsKey.OrderMakerIP),
                LogFile = GetSettingValue(AppSettingsKey.LogFile)
            };

            LoadTradeSettings();
        }

        private BrokageSettings _brokageSettings;
        public BrokageSettings BrokageSettings => _brokageSettings;

        public string LogFilePath => GetSettingValue(AppSettingsKey.LogFile);

        public string SecurityKey => GetSettingValue(AppSettingsKey.SecurityKey);

        public bool CheckBasicSetting()
        {
            string sid = BrokageSettings.SID;
            if (String.IsNullOrEmpty(sid)) return false;

            string password = BrokageSettings.Password;
            if (String.IsNullOrEmpty(password)) return false;

            return true;
        }

        string DecryptPassword(string val)
        {
            try
            {
                string password = CryptoGraphy.DecryptCipherTextToPlainText(val, SecurityKey);
                return password;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public List<TradeSettings> TradeSettings => _tradeSettings;
        public TradeSettings FindTradeSettings(string id) => TradeSettings.FirstOrDefault(x => x.Id == id);

        void LoadTradeSettings()
        {
            Directory.CreateDirectory(TradeSettingsFolder);

            if (!File.Exists(TradeSettingsPath)) File.Create(TradeSettingsPath).Close();

            string content = "";
            using (StreamReader sr = new StreamReader(TradeSettingsPath))
            {
                content = sr.ReadToEnd();
            }

            var tradeSettings = JsonConvert.DeserializeObject<List<TradeSettings>>(content);
            if (tradeSettings.HasItems()) this._tradeSettings = tradeSettings;

        }

        public void AddUpdateTradeSettings(TradeSettings tradeSettings)
        {
            if (String.IsNullOrEmpty(tradeSettings.Id))
            {
                //新增
                tradeSettings.Id = Guid.NewGuid().ToString();
                _tradeSettings.Add(tradeSettings);
            }
            else
            {
                var idx = _tradeSettings.FindIndex(x => x.Id == tradeSettings.Id);
                _tradeSettings[idx] = tradeSettings;
            }

            SaveTradeSettings();

        }

        public void RemoveTradeSettings(TradeSettings tradeSettings)
        {
            var idx = _tradeSettings.FindIndex(x => x.Id == tradeSettings.Id);
            _tradeSettings.RemoveAt(idx);

            SaveTradeSettings();

        }

        void SaveTradeSettings()
        {
            using (StreamWriter file = File.CreateText(TradeSettingsPath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, _tradeSettings);
            }
        }

    }


}
