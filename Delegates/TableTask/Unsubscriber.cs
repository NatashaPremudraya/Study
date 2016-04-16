using System;
using System.Collections.Generic;

namespace TableTask
{
    internal class Unsubscriber<ITable> : IDisposable
    {
        private List<IObserver<ITable>> observers;
        private IObserver<ITable> observer;

        internal Unsubscriber(List<IObserver<ITable>> observers, IObserver<ITable> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
    }
}