using System;

namespace Delegates
{
    public abstract class TransactionProcessor
    {
        public Transaction Process(TransactionRequest request, IProcessor processor)
        {
            if (!processor.Check(request))
                throw new ArgumentException();
            var result = processor.Register(request);
            processor.Save(result);
            return result;
        }
    }
}