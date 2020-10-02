using System;
using System.Collections.Generic;
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


            StringBuilder sb = new StringBuilder();
            byte[] buff = File.ReadAllBytes(openFilePath);
            if (buff[0] != 0x01)
            {
                Version = "Zła wersja";
            }
            else
            {
                Version = BitConverter.ToString(buff, 0, 1);
                SerialNumber = BitConverter.ToString(buff, 1, 3).Replace("-", string.Empty);

                var b1 = new byte[4];
                Array.Copy(buff, 4, b1, 0, 4);
                var b2 = b1.Reverse();
                var time1 = BitConverter.ToInt32(b2.ToArray(), 0);
                Time = DateTimeOffset.FromUnixTimeSeconds(time1).ToString();

                Counter = BitConverter.ToString(buff, 8, 4).Replace("-", string.Empty);

                for (int i = 16; i < buff.Length; i++)
                {
                    if (BitConverter.ToString(buff, i, 1) == "CC")
                        sb.Append(i - 16 + " ");
                }

                ActiveProgramms = sb.ToString();
            }
        }
    }
}
