using System;

namespace Delegates
{
    public abstract class TransactionProcessor
    {
        public Transaction Process(TransactionRequest request)
        {
            if (!Check(request))
                throw new ArgumentException();
            var result = Register(request);
            Save(result);
            return result;
        }

        protected abstract bool Check(TransactionRequest request);
        protected abstract Transaction Register(TransactionRequest request);
        protected abstract void Save(Transaction transaction);
    }
}