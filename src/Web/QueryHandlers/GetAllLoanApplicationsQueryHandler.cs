using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Employee.Core.ApplicationServices;
using Employee.Projections;
using Employee.Requests;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;

namespace Web.QueryHandlers
{


    public class GetAllEmployeesQueryHandler : IQueryHandler<GetAllEmployeesQuery, EmployeeDto[]>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllEmployeesQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<EmployeeDto[]> ExecuteQueryAsync(GetAllEmployeesQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<EmployeeReadModel>(
                        Label.Named("mssql-fetch-loanapp-read-model"),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Employee]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToLoanApplication()).ToArray();
        }
    }
}
