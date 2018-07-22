using System;

namespace Task1.Logic
{
    public abstract class BaseClockObserver
    {
        /// <summary>
        /// Add new listener
        /// </summary>
        /// <param name="clock"> Object for listening </param>
        public void Register(Clock clock)
        {
            if (clock == null)
            {
                throw new ArgumentNullException($"Paramentr {nameof(clock)} can't be null!");
            }

            clock.TimeOutChange += TimeOutChanged;
        }

        /// <summary>
        /// Delete listener
        /// </summary>
        /// <param name="clock"> Object for listening </param>
        public void UnRegister(Clock clock)
        {
            if (clock == null)
            {
                throw new ArgumentNullException($"Paramentr {nameof(clock)} can't be null!");
            }

            clock.TimeOutChange -= TimeOutChanged;
        }

        /// <summary>
        /// Execute when clock time out event is happened
        /// </summary>
        /// <param name="sender"> Object who created event </param>
        /// <param name="e"> Event inforfation </param>
        protected abstract void TimeOutChanged(object sender, TimeOutArgs e);
    }
}
