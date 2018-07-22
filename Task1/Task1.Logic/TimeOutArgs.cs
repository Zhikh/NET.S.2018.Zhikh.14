using System;

namespace Task1.Logic
{
    public class TimeOutArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public TimeOutArgs(int hours, int minutes, int seconds)
        {
            if (hours < 0)
            {
                throw new ArgumentException($"The parametr {nameof(hours)} can't consist negative value!");
            }

            if (minutes < 0)
            {
                throw new ArgumentException($"The parametr {nameof(minutes)} can't consist negative value!");
            }

            if (seconds < 0)
            {
                throw new ArgumentException($"The parametr {nameof(seconds)} can't consist negative value!");
            }

            Message = $"Timer is on {hours} : {minutes} : {seconds}.";
        }

        public string Message { get; set; }
    }
}
