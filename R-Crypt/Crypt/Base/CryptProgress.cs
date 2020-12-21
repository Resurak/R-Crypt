using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using R_Crypt.Common.Utils;

namespace R_Crypt.Crypt.Base
{
    public class CryptProgress
    {
        public CryptProgress(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            TotalByte = info.Length;

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private string currentByteUnit = "";
        private string totalByteUnit = "";

        private double currentByteConverted;
        private double totalByteConverted;
        private double precedentByte;

        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan TS_Total = new TimeSpan();
        TimeSpan TS_Remaining = new TimeSpan();

        public long CurrentByte { get; set; }
        public long TotalByte { get; set; }
        public bool Completed { get; set; }

        //public string ProgressText
        //{
        //    get
        //    {
        //        if (CurrentByte == 0 && TotalByte == 0) return "--B / --B";
        //        if (TotalByte != 0)
        //        {
        //            totalByteConverted = TotalByte.ByteConversion();
        //            totalByteUnit = TotalByte.GetByteUnit();

        //            currentByteConverted = CurrentByte.ByteConversion();
        //            currentByteUnit = CurrentByte.GetByteUnit();

        //            return $"{Math.Round(currentByteConverted, 2)}{currentByteUnit} / {Math.Round(totalByteConverted, 2)}{totalByteUnit}";
        //        }
        //        else return "--B / --B";
        //    }
        //}

        //public string CompletedText
        //{
        //    get
        //    {
        //        if (Completed) return "File cifrato con successo!";
        //        else return "Cifrando il file";
        //    }
        //}

        public string TotalTime { get => $"{TS_Total.Minutes}m : {TS_Total.Seconds}s"; }
        public string RemainingTime { get => $"{TS_Remaining.Minutes}m : {TS_Remaining.Seconds}s"; }

        public void StartTimer()
        {
            timer.Start();
        }

        int seconds = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            double difference = currentByteConverted - precedentByte;
            double totalMinusConverted = totalByteConverted - currentByteConverted;

            int totalSeconds = (int)(totalMinusConverted / difference);

            TS_Remaining = TimeSpan.FromSeconds(totalSeconds);
            TS_Total = TimeSpan.FromSeconds(seconds);

            seconds++;

            precedentByte = currentByteConverted;
        }

        public void StopTimer()
        {
            timer.Stop();
        }

    }
}
