using System;

namespace Delegates
{
    public class TransactionProcessor
    {
        public Transaction Process(IRequest request, IProcessor processor)
        {
            if (!processor.Check(request))
                throw new ArgumentException();
            var result = processor.Register(request);
            processor.Save(result);
            return result;
        }
    }
}