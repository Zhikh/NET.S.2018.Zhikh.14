using System;

namespace Task1.Logic
{
    public class Clock
    {
        private const int MAX_TICK = 60;
        private const int DEFAULT = 0;

        private int _hours;
        private int _minutes;
        private int _seconds;

        public event EventHandler<TimeOutArgs> TimeOutChange = delegate { };
        
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int Hours
        {
            get
            {
                return _hours;
            }

            private set
            {
                if (value < 0)
                {
                    _hours = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Minutes
        {
            get
            {
                return _minutes;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(nameof(Minutes) + " cant't be less than 0!");
                }

                Hours += value / ( MAX_TICK + 1);
                _minutes = value % ( MAX_TICK + 1);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Seconds
        {
            get
            {
                return _seconds;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(nameof(Minutes) + " cant't be less than 0!");
                }

                Minutes += value / (MAX_TICK + 1);
                _seconds = value % (MAX_TICK + 1);
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public void SetClock(int seconds = 0, int minutes = 0, int hours = 0)
        {
            if (hours == 0 && minutes == 0 && seconds == 0)
            {
                throw new ArgumentException("Also one parametr should be set!");
            }

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;

            Start();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void ResetClock()
        {
            Hours = DEFAULT;
            Minutes = DEFAULT;
            Seconds = DEFAULT;

            OnTimeOutChange(this, new TimeOutArgs(Hours, Minutes, Seconds));
        }
        #endregion

        #region Virtual methods
        protected virtual void OnTimeOutChange(object sender, TimeOutArgs eventArgs)
        {
            TimeOutChange?.Invoke(this, eventArgs);
        }
        #endregion

        #region Private methods
        private void Start()
        {
            bool isStop = false;

            do
            {
                if (--Seconds == 0)
                {
                    isStop = ResetSeconds();
                }
            }
            while (!isStop);

            OnTimeOutChange(this, new TimeOutArgs(Hours, Minutes, Seconds));
        }

        private bool ResetSeconds()
        {
            if (Minutes != 0)
            {
                Minutes--;
                Seconds = MAX_TICK;
            }
            else
            {
                if (Hours != 0)
                {
                    Hours--;
                    Minutes = 59;
                    Seconds = MAX_TICK;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
