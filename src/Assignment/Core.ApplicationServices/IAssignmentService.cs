using System.Threading.Tasks;
using Assignment.Core.DomainModel;
using Assignment.Response;

namespace Assignment.Core.ApplicationServices
{
    public interface IAssignmentService
    {
        Task CreateAssignment(CreateAssignmentCommand command);
        Task<AssignmentDto[]> GetAll();
        Task SetWaitingForPayment(WaitForPaymentCommand command);
    }
}
