using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactionProcessor = new TransactionProcessor();
            var processor = new Processor();
            var orderTransaction = transactionProcessor.Process(new Order(), processor);
            var orderRequestTransaction = transactionProcessor.Process(new OrderRequest(), processor);
            var requestTransaction = transactionProcessor.Process(new TransactionRequest(), processor);
        }
    }
}
