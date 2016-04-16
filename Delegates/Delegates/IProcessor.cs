namespace Delegates
{
    public interface IProcessor
    {
        bool Check(TransactionRequest request);
        Transaction Register(TransactionRequest request);
        void Save(Transaction transaction);
    }
}