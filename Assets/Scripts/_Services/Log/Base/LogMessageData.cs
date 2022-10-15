using UnityEngine;

namespace Services.Log
{
    public class LogMessageData 
    {
        public string Prefix;

        public string ItemName;

        public string Message;

        public LogType Type;

        public Color Color;

        public LogMessageData(string prefix, string itemName, string message, LogType logType, Color color) 
        {
            Prefix = prefix;
            ItemName = itemName;
            Message = message;
            Type = logType;
            Color = color; 
        }

        public override string ToString()
        {
            var itemNameWithColor = string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", 
                (byte)(Color.r * 255f), (byte)(Color.g * 255f), (byte)(Color.b * 255f), ItemName);

            var logType = "";

            switch (Type)
            {
                case LogType.Message:
                    { 
                        logType = string.Format("<color=white>{0}</color>", Type.ToString());
                        break; 
                    }
                case LogType.Warning:
                    {
                        logType = string.Format("<color=yellow>{0}</color>", Type.ToString());
                        break; 
                    }
                case LogType.Error:
                    {
                        logType = string.Format("<color=red>{0}</color>", Type.ToString());
                        break;
                    }
            }

            return $"[{Prefix}]->[{itemNameWithColor}]->[{logType}]->[{Message}]";
        }
    }
}
