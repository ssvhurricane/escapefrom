using Data.Settings;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Services.Log
{
    public class LogService
    {
        private readonly SignalBus _signalBus;

        private readonly LogServiceSettings _logServiceSettings;

        public LogService(SignalBus signalBus, LogServiceSettings logServiceSettings)
        {
            _signalBus = signalBus;

            _logServiceSettings = logServiceSettings;
        }

        public void ShowLog(string itemName,
            LogType logType,
            string message,
            LogOutputLocationType logOutputLocationType = LogOutputLocationType.Console) 
        {
            if (!_logServiceSettings.EnableAll) return;

            var logItemData = _logServiceSettings.LogItemDatas.FirstOrDefault(item => item.Name == itemName);

            if (logItemData == null || !logItemData.Enable) return;

            switch (logType) 
            {
                case LogType.Message: 
                    {
                        MessageProcess(new LogMessageData("Log",itemName, message, logType, logItemData.Color), logOutputLocationType);
                        break;
                    }

                case LogType.Warning: 
                    {
                        WarningProcess(new LogMessageData("Log", itemName, message, logType, logItemData.Color), logOutputLocationType);
                        break;
                    }
                case LogType.Error: 
                    {
                        ErrorProcess(new LogMessageData("Log", itemName, message, logType, logItemData.Color), logOutputLocationType);
                        break;
                    }
            }
        }

        private void MessageProcess(LogMessageData logMessageData, LogOutputLocationType logOutputLocationType)
        {
            switch (logOutputLocationType)
            {
                case LogOutputLocationType.All: 
                    {
                        // TODO:
                        break;
                    }
                case LogOutputLocationType.Console: 
                    {
                        Debug.Log(logMessageData.ToString());

                        break;
                    }
                case LogOutputLocationType.File: 
                    {
                        // TODO:
                        break; 
                    }
                case LogOutputLocationType.Email: 
                    {
                        // TODO:
                        break;
                    }
            }
        }

        private void WarningProcess(LogMessageData logMessageData, LogOutputLocationType logOutputLocationType)
        {
            switch (logOutputLocationType)
            {
                case LogOutputLocationType.All:
                    {
                        // TODO:
                        break;
                    }
                case LogOutputLocationType.Console:
                    {
                        Debug.LogWarning(logMessageData.ToString());

                        break;
                    }
                case LogOutputLocationType.File:
                    {
                        // TODO:
                        break;
                    }
                case LogOutputLocationType.Email:
                    {
                        // TODO:
                        break;
                    }
            }
        }
        private void ErrorProcess(LogMessageData logMessageData, LogOutputLocationType logOutputLocationType)
        {
            switch (logOutputLocationType)
            {
                case LogOutputLocationType.All:
                    {
                        // TODO:
                        break;
                    }
                case LogOutputLocationType.Console:
                    {
                        Debug.LogError(logMessageData.ToString());

                        break;
                    }
                case LogOutputLocationType.File:
                    {
                        // TODO:
                        break;
                    }
                case LogOutputLocationType.Email:
                    {
                        // TODO:
                        break;
                    }
            }
        }
    }
}
