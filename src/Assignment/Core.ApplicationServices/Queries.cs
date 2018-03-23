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
        public Guid Id { get; }

        public GetAssignmentQuery(Guid id)
        {
            Id = id;
        }
    }


}