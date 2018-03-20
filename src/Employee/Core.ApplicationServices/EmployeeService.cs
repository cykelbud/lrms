using System;
using System.Threading;
using System.Threading.Tasks;
using Employee.Core.DomainModel;
using Employee.Requests;
using EventFlow;
using EventFlow.Queries;

namespace Employee.Core.ApplicationServices
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public EmployeeService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task<Guid> CreateEmployee(CreateEmployeeRequest request)
        {
            var aggregateId = EmployeeId.New;
            await _commandBus.PublishAsync(
                new CreateEmployeeCommand(aggregateId, request.UserName, request.PersonalIdentificationNumber),
                CancellationToken.None);
            return aggregateId.GetGuid();
        }

        public async Task AddBankInfo(AddEmployeeBankInfoRequest request)
        {
            var employeeId = EmployeeId.With(request.EmployeeId);
            await _commandBus.PublishAsync(new AddBankInfoCommand(employeeId, request.BankAccountNumber),
                CancellationToken.None);
        }

        public async Task<EmployeeDto[]> GetAll()
        {
            var employees = await _queryProcessor.ProcessAsync(new GetAllEmployeesQuery(), CancellationToken.None);
            return employees;
        }

        public async Task<EmployeeDto> GetEmployee(Guid employeeId)
        {
            var employee = await _queryProcessor.ProcessAsync(new GetEmployeeQuery(employeeId), CancellationToken.None);
            return employee;
        }
    }
}