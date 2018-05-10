namespace Invoice.Core.ApplicationServices
{
    public interface IInvoicePrinter
    {
        void PrintInvoice(string fromName, string toAddress, decimal amount);
        void PrintReminder(string employeeName, string customerAddress);
    }

    public class InvoicePrinter : IInvoicePrinter
    {
        public void PrintInvoice(string fromName, string toAddress, decimal amount)
        {
            
        }

        public void PrintReminder(string employeeName, string customerAddress)
        {
            
        }
    }
}