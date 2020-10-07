using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace LimitsConverter
{
    class ProgramStatus

    {
        public int Version { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Time { get; set; }
        public int Counter { get; set; }
        public List<int> ActiveProgramms { get; set; }
        public ProgramStatus(string openFilePath)
        {

            byte[] buff = File.ReadAllBytes(openFilePath);

            if (buff[0] == 0x01)
            {
                Version = SetVersion(buff);
                SerialNumber = SetSerialNumber(buff);
                Time = SetTime(buff);

                Counter = CounterActivation(buff);
                ActiveProgramms = SetActiveProgramms(buff);
            }
            else
            {
                throw new ArgumentException();
            }

        }

        private int SetVersion(byte[] byteArrayToBuff)
        {

            return byteArrayToBuff[0];
        }
        private string SetSerialNumber(byte[] byteArrayToBuff)
        {
            return BitConverter.ToString(byteArrayToBuff, 1, 3).Replace("-", string.Empty);
        }
        private DateTime SetTime(byte[] byteArrayToBuff)
        {
            byte[] byteBufforToTime = new byte[4];
            Array.Copy(byteArrayToBuff, 4, byteBufforToTime, 0, 4);
            var correctArrayToReverseBytes = byteBufforToTime.Reverse();
            int timeArray = BitConverter.ToInt32(correctArrayToReverseBytes.ToArray(), 0);
            return DateTimeOffset.FromUnixTimeSeconds(timeArray).DateTime;
        }


        private int CounterActivation(byte[] byteArrayToBuff)
        {
            return BitConverter.ToInt32(byteArrayToBuff,8);
        }
        private List<int> SetActiveProgramms(byte[] byteArrayToBuff)
        {

            List<int> listOfProgramms = new List<int>();
            StringBuilder sb = new StringBuilder();
            for (int i = 16; i < byteArrayToBuff.Length; i++)
            {
                if (BitConverter.ToString(byteArrayToBuff, i, 1) == "CC")
                listOfProgramms.Add(i - 16);
            }

            return listOfProgramms;
                
            
        }
    }
}
