using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Assignment.Core.ApplicationServices;
using Assignment.Core.DomainModel;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Employee.Requests;
using EventFlow.Queries;
using Invoice.Requests;
using Microsoft.Extensions.DependencyInjection;
using Payment.Core.ApplicationServices;
using Payment.Requests;
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
        private readonly PayoutController _payoutController;
        private IAssignmentService _assignmentService;
        private IQueryProcessor _queryProcessor;
        private IPaymentService _paymentService;

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

            _queryProcessor = _serviceProvider.GetService<IQueryProcessor>();
            _assignmentService = _serviceProvider.GetService<IAssignmentService>();
            _paymentService = _serviceProvider.GetService<IPaymentService>();

            _invoiceController = _serviceProvider.GetService<InvoiceController>();
            _employeeController = _serviceProvider.GetService<EmployeeController>();
            _customerController = _serviceProvider.GetService<CustomerController>();
            _paymentController = _serviceProvider.GetService<PaymentController>();
            _payoutController = _serviceProvider.GetService<PayoutController>();
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
                InvoiceItems = new List<InvoiceItemDto>() { new InvoiceItemDto() {Price = 10000.0m, Description = "städning"} }.ToArray(),
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Name = "invoiceName",
                Vat = 25m,
                InvoiceDescription = "invoice description"
            };
            var invoiceId = await _invoiceController.CreateInvoice(createInvoiceRequest);

            await _invoiceController.SendInvoice(invoiceId);

            var payments = await _paymentController.GetAll();

            // simulate payment
            await _paymentController.SimulateReceivePayment(new ReceivePaymentRequest() {InvoiceId = invoiceId});

            var payouts = await _payoutController.GetAll();


        }


        [Fact]
        public async Task CreateAssignments()
        {
            var invoiceId = Guid.NewGuid();
            var assignmentId = AssignmentId.New;
            await _assignmentService.CreateAssignment(new CreateAssignmentCommand(assignmentId, invoiceId));
            var assignment = await _queryProcessor.ProcessAsync(new GetAssignmentByInvoiceIdQuery(invoiceId), CancellationToken.None);
            Assert.Equal(assignmentId.GetGuid(), assignment.AssignmentId);
        }


        [Fact]
        public async Task CreateAssignments2()
        {
            var invoiceId = Guid.NewGuid();
            var assignmentId = AssignmentId.New;
            await _assignmentService.CreateAssignment(new CreateAssignmentCommand(assignmentId, invoiceId));
            var assignment = await _queryProcessor.ProcessAsync(new GetAssignmentByInvoiceIdQuery(invoiceId), CancellationToken.None).ConfigureAwait(false);
            Assert.Equal(assignmentId.GetGuid(), assignment.AssignmentId);

            await _assignmentService.SetWaitingForPayment(new WaitForPaymentCommand(AssignmentId.With(assignment.AssignmentId), assignment.InvoiceId)).ConfigureAwait(false);
           // await _paymentService.SetWaitingForPayment(new WaitingForPaymentRequest() { InvoiceId = invoiceId }).ConfigureAwait(false);

        }

        [Fact]
        public async Task CreateAssignments3()
        {
            var invoiceId = Guid.NewGuid();
            var assignmentId = AssignmentId.New;
            await _assignmentService.CreateAssignment(new CreateAssignmentCommand(assignmentId, invoiceId));
            var assignment = await _queryProcessor.ProcessAsync(new GetAssignmentByInvoiceIdQuery(invoiceId), CancellationToken.None).ConfigureAwait(false);
            Assert.Equal(assignmentId.GetGuid(), assignment.AssignmentId);

            await _assignmentService.SetWaitingForPayment(new WaitForPaymentCommand(assignmentId, invoiceId)).ConfigureAwait(false);
        }


        [Fact]
        public async Task CreateAssignments4()
        {
            var invoiceId = new Guid("743de7e2-7379-48f0-b113-1f3879bacb80");
            var assignmentId = AssignmentId.With(new Guid("d8f2b0ea-6676-4afe-ab34-b8c41639d7e9"));
            await _assignmentService.SetWaitingForPayment(new WaitForPaymentCommand(assignmentId, invoiceId)).ConfigureAwait(false);
        }



        [Fact]
        public async Task WaitForPayment()
        {
            var invoiceId = Guid.NewGuid();
            await _paymentService.SetWaitingForPayment(new WaitingForPaymentRequest() { InvoiceId = invoiceId }).ConfigureAwait(false);
        }


        public void Dispose()
        {
            _invoiceController?.Dispose();
            _employeeController?.Dispose();
            _autofacContainer?.Dispose();
            _paymentController?.Dispose();
            _payoutController?.Dispose();
        }
    }

}
