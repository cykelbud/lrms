using System;

namespace Payment.Response
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid InvoiceId { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public PaymentState CurrentState { get; set; }
        public decimal Amount { get; set; }
    }

    public enum PaymentState
    {
        WaitingForPayment,
        PaymentReceived,
        PaymentDue,
        DebtCollection, // inkasso
        PaymentInjuction, // betalnings föreläggande
        Distraint, // utmätning
        PendingPayments,
    }
}