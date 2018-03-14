using System;
using System.Threading.Tasks;
using EmployeeService.Requests;

namespace EmployeeService.Core.ApplicationServices
{
    public interface IEmployeeService
    {
        Task<Guid> CreateEmployee(CreateEmployeeRequest request);
    }
}
