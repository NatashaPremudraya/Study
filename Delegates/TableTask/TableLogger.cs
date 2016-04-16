using System;

namespace TableTask
{
    public class TableLogger : IObserver<ITable>
    {
        ITable table;

        public void OnNext(ITable table)
        {
            ;
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