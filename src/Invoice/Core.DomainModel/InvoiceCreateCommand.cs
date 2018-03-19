using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;

namespace Invoice.Core.DomainModel
{
    public class InvoiceCreateCommand : Command<InvoiceAggregate,InvoiceId>
    {
        public Guid EmployeeId { get; }
        public Guid CustomerId { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string InvoiceDescription { get; }
        public string Name { get; }
        public decimal Vat { get; }
        public InvoiceItem[] InvoiceItems { get; }

        public InvoiceCreateCommand(InvoiceId aggregateId, Guid employeeId, Guid customerId, DateTime startDate, DateTime endDate, string invoiceDescription, string name, decimal vat, InvoiceItem[] invoiceItems) : base(aggregateId)
        {
            EmployeeId = employeeId;
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
            InvoiceDescription = invoiceDescription;
            Name = name;
            Vat = vat;
            InvoiceItems = invoiceItems;
        }

        public InvoiceCreateCommand(InvoiceId aggregateId, ISourceId sourceId, Guid employeeId, Guid customerId, DateTime startDate, DateTime endDate, string invoiceDescription, string name, decimal vat, InvoiceItem[] invoiceItems) : base(aggregateId, sourceId)
        {
            EmployeeId = employeeId;
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
            InvoiceDescription = invoiceDescription;
            Name = name;
            Vat = vat;
            InvoiceItems = invoiceItems;
        }
    }

    public class InvoiceCreateCommandHandler : CommandHandler<InvoiceAggregate, InvoiceId, InvoiceCreateCommand>
    {
        public override Task ExecuteAsync(InvoiceAggregate aggregate, InvoiceCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.CreateCustomer(command);
            return Task.FromResult(0);
        }
    }
}