﻿using System;
using EventFlow.Aggregates;

namespace InvoiceService.Core.DomainModel
{
    public class InvoiceAggregate : 
        AggregateRoot<InvoiceAggregate, InvoiceId>,
        IApply<InvoiceCreatedEvent>
    {
        // state
        public Guid CustomerId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string InvoiceDescription { get; private set; }
        public string InvoiceName { get; private set; }
        public decimal Vat { get; private set; }
        public InvoiceItem[] InvoiceItems { get; private set; }

        public InvoiceAggregate(InvoiceId id) : base(id)
        {
        }

        public void CreateCustomer(InvoiceCreateCommand command)
        {
            Emit(new InvoiceCreatedEvent(command.EmployeeId, command.CustomerId, command.StartDate, command.EndDate,
                command.InvoiceDescription, command.Name, command.Vat, command.InvoiceItems));
        }

        public void SendInvoice(InvoiceSendCommand command)
        {
            Emit(new InvoiceSentEvent());
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
        }


    }
}
