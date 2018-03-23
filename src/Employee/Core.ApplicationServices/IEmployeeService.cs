using System;
using System.Threading.Tasks;
using Employee.Requests;
using Employee.Response;

namespace Employee.Core.ApplicationServices
{
    public interface IEmployeeService
    {
        Task<Guid> CreateEmployee(CreateEmployeeRequest request);
        Task AddBankInfo(AddEmployeeBankInfoRequest request);
        Task<EmployeeDto[]> GetAll();
        Task<EmployeeDto> GetEmployee(Guid employeeId);
    }
}
