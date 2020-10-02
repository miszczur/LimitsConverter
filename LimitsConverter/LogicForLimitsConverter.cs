using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace LimitsConverter
{
    class LogicForLimitsConverter

    {
        public string Version { get; set; }
        public string SerialNumber { get; set; }
        public string Time { get; set; }
        public string Counter { get; set; }
        public string ActiveProgramms { get; set; }
        public LogicForLimitsConverter(string openFilePath)
        {


            
            byte[] buff = File.ReadAllBytes(openFilePath);
            if (buff[0] != 0x01)
            {
                Version = "Zła wersja";
            }
            else
            {
                Version = SetVersion(buff);
                SerialNumber = SetSerialNumber(buff);
                Time = SetTime(buff);

                Counter = SetCounter(buff);
                ActiveProgramms = SetActiveProgramms(buff);
            }
        }

       

        private string SetTime(byte[]byteArrayToBuff)
        {
            var byteBufforToTime = new byte[4];
            Array.Copy(byteArrayToBuff, 4, byteBufforToTime, 0, 4);
            var correctArrayToReverseBytes = byteBufforToTime.Reverse();
            var timeArray = BitConverter.ToInt32(correctArrayToReverseBytes.ToArray(), 0);
            return DateTimeOffset.FromUnixTimeSeconds(timeArray).ToString();
        }
        private string SetVersion(byte[] byteArrayToBuff)
        {
            return BitConverter.ToString(byteArrayToBuff, 0, 1);
        }
        private string SetSerialNumber(byte[] byteArrayToBuff)
        {
            return BitConverter.ToString(byteArrayToBuff, 1, 3).Replace("-", string.Empty);
        }
        private string SetCounter(byte[] byteArrayToBuff)
        {
            return BitConverter.ToString(byteArrayToBuff, 8, 4).Replace("-", string.Empty);
        }
        private string SetActiveProgramms(byte[] byteArrayToBuff)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 16; i < byteArrayToBuff.Length; i++)
            {
                

                if (BitConverter.ToString(byteArrayToBuff, i, 1) == "CC")
                    sb.Append(i - 16 + " ");
            }
            return sb.ToString();
        }
    }
}
