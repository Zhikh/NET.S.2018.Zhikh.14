using System;
using Task1.Logic;

namespace Task1.UI.ConsoleApp
{
    public class FakeObserver : BaseClockObserver
    {
        protected sealed override void TimeOutChanged(object sender, TimeOutArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
