using System;
using Assignment.Response;
using EventFlow.Queries;

namespace Assignment.Core.ApplicationServices
{
    public class GetAllAssignmentsQuery : IQuery<AssignmentDto[]>
    {
    }

    public class GetAssignmentQuery : IQuery<AssignmentDto>
    {
        public Guid AssignmentId { get; }

        public GetAssignmentQuery(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }
    }

    public class GetAssignmentByInvoiceIdQuery : IQuery<AssignmentDto>
    {
        public Guid InvoiceId { get; }

        public GetAssignmentByInvoiceIdQuery(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }


    public class GetAssignmentInvoiceQuery : IQuery<AssignmentInvoiceDto>
    {
        public Guid InvoiceId { get; }

        public GetAssignmentInvoiceQuery(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }

}