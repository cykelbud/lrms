using System;
using AssignmentService.Requests;

namespace AssignmentService.Core.DomainApplications
{
    public interface IAssignmentService
    {
        Guid CreateAssignment(CreateAssignmentRequest request);
    }
}
