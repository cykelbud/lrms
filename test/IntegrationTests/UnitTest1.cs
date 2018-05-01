
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Employee.Requests;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using Invoice.Requests;
using Microsoft.Extensions.DependencyInjection;
using Web.Configuration;
using Web.Controllers;
using Xunit;

namespace IntegrationTests
{
    public class UnitTest1 : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IContainer _autofacContainer;
        private readonly InvoiceController _invoiceController;
        private readonly EmployeeController _employeeController;
        private readonly CustomerController _customerController;
        private readonly PaymentController _paymentController;

        public UnitTest1()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddApplicationServices();
            services.AddTransient<InvoiceController>();
            services.AddTransient<EmployeeController>();
            services.AddTransient<CustomerController>();
            services.AddTransient<PaymentController>();

            _autofacContainer = services.AddAutofacContainer();
            _serviceProvider = new AutofacServiceProvider(_autofacContainer);


            _invoiceController = _serviceProvider.GetService<InvoiceController>();
            _employeeController = _serviceProvider.GetService<EmployeeController>();
            _customerController = _serviceProvider.GetService<CustomerController>();
            _paymentController = _serviceProvider.GetService<PaymentController>();
        }



        [Fact]
        public async Task Test1()
        {
            var request = new CreateEmployeeRequest()
            {
                PersonalIdentificationNumber = "1234567890",
                UserName = "name",
            };
            var employeeId = await _employeeController.CreateEmployee(request);

            var customerRequest = new CreateCustomerRequest()
            {
                EmployeeId = employeeId,
                PersonalIdentificationNumber = "customer ssn",
                UserName = "customer name",
                Address = "address"
            };
            var customerId = await _customerController.CreateCustomer(customerRequest);

            var createInvoiceRequest = new CreateInvoiceRequest()
            {
                EmployeeId   = employeeId,
                CustomerId = customerId,
                InvoiceItems = new List<InvoiceItemDto>() { new InvoiceItemDto() {Price = 5.0m, Description = "item"} }.ToArray(),
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Name = "invoiceName",
                Vat = 25m,
                InvoiceDescription = "invoice description"
            };
            var invoiceId = await _invoiceController.CreateInvoice(createInvoiceRequest);

            await _invoiceController.SendInvoice(invoiceId);

            var payments = await _paymentController.GetAll();

        }

        public void Dispose()
        {
            _invoiceController?.Dispose();
            _employeeController?.Dispose();
            _autofacContainer?.Dispose();
            _paymentController?.Dispose();
        }
    }

}
