using System;
using Task1.Logic;

namespace Task1.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var observer = new FakeObserver();
            var anotherObserver = new FakeObserver();
            var clock = new Clock();
            
            observer.Register(clock);
            anotherObserver.Register(clock);

            clock.SetClock(45, 45);

            Console.WriteLine($"The {nameof(observer)} unregister.");
            observer.UnRegister(clock);

            clock.SetClock(30, 1);

            Console.WriteLine($"The {nameof(anotherObserver)} unregister.");
            anotherObserver.UnRegister(clock);
            clock.SetClock(minutes: 1);

            Console.ReadKey();
        }
    }
}
