using System;

namespace Delegates
{
    public class Processor : IProcessor
    {
        public bool Check(IRequest request)
        {
            throw new NotImplementedException();
        }

        public Transaction Register(IRequest request)
        {
            throw new NotImplementedException();
        }

        public void Save(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}