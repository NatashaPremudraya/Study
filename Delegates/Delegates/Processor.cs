using System;

namespace Delegates
{
    public class Processor : IProcessor
    {
        public bool Check(TransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Transaction Register(TransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public void Save(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}