using System;

namespace TableTask
{
    public class TableGui : IObserver<ITable>
    {
        ITable table;

        public void OnNext(ITable table)
        {
            this.table = table;
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}