using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeService.Core.DomainModel;
using EmployeeService.Requests;
using EventFlow;

namespace EmployeeService.Core.ApplicationServices
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly ICommandBus _commandBus;

        public EmployeeService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task<Guid> CreateEmployee(CreateEmployeeRequest request)
        {
            var aggregateId = EmployeeId.New;
            await _commandBus.PublishAsync(
                new CreateEmployeeCommand(aggregateId, request.UserName, request.PersonalIdentificationNumber),
                CancellationToken.None);
            return aggregateId.GetGuid();
        }

        public Task AddBankInfo(AddEmployeeBankInfoRequest request)
        {
            var employeeId = EmployeeId.With(request.EmployeeId);
           
            throw new NotImplementedException();
        }
    }
}