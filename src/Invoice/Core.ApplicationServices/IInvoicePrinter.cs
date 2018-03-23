namespace Invoice.Core.ApplicationServices
{
    public interface IInvoicePrinter
    {
        void PrintInvoice(string fromName, string toAddress, decimal amount);
    }

    public class InvoicePrinter : IInvoicePrinter
    {
        public void PrintInvoice(string fromName, string toAddress, decimal amount)
        {
            
        }
    }
}