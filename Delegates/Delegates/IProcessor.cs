namespace Delegates
{
    public interface IProcessor
    {
        bool Check(IRequest request);
        Transaction Register(IRequest request);
        void Save(Transaction transaction);
    }
}