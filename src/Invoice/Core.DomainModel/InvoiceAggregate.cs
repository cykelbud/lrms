using System;
using EventFlow.Aggregates;

namespace Invoice.Core.DomainModel
{
    public class InvoiceAggregate : 
        AggregateRoot<InvoiceAggregate, InvoiceId>,
        IApply<InvoiceCreatedEvent>,
        IApply<InvoiceSentEvent>,
        IApply<InvoiceReminderSentEvent>
    {
        // state
        public Guid CustomerId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string InvoiceDescription { get; private set; }
        public string InvoiceName { get; private set; }
        public decimal Vat { get; private set; }
        public InvoiceItem[] InvoiceItems { get; private set; }
        public bool IsSent { get; set; }
        public bool PayInAdvance { get; set; }
        public bool RemiderSent { get; set; }
        public bool HasTaxReduction { get; set; } // RUT / ROT
        

        public InvoiceAggregate(InvoiceId id) : base(id)
        {
        }

        public void CreateInvoice(InvoiceCreateCommand command)
        {
            Emit(new InvoiceCreatedEvent(command.EmployeeId, command.CustomerId, command.StartDate, command.EndDate,
                command.InvoiceDescription, command.Name, command.Vat, command.InvoiceItems, command.PayInAdvance, command.HasTaxReduction));
        }


        public void Apply(InvoiceCreatedEvent e)
        {
            CustomerId = e.CustomerId;
            StartDate = e.StartDate;
            EndDate = e.EndDate;
            InvoiceDescription = e.InvoiceDescription;
            InvoiceName = e.Name;
            Vat = e.Vat;
            InvoiceItems = e.InvoiceItems;
            PayInAdvance = e.PayInAdvance;
            HasTaxReduction = e.HasTaxReduction;
        }
        
        public void SendInvoice(InvoiceSendCommand command)
        {
            Emit(new InvoiceSentEvent(DateTime.Now));
        }

        public void Apply(InvoiceSentEvent aggregateEvent)
        {
            IsSent = true;
        }

        public void SendReminder(InvoiceReminderCommand command)
        {
            Emit(new InvoiceReminderSentEvent(DateTime.Now));
        }

        public void Apply(InvoiceReminderSentEvent aggregateEvent)
        {
            RemiderSent = true;
        }

    }
}
