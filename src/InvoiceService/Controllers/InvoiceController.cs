﻿using System;
using System.Threading.Tasks;
using InvoiceService.Core.ApplicationServices;
using InvoiceService.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceService.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("")]
        public async Task CreateInvoice([FromBody] CreateInvoiceRequest request)
        {
            await _invoiceService.CreateInvoice(request);
        }

        [HttpPost("send/{invoiceId}")]
        public async Task SendInvoice([FromBody] Guid invoiceId)
        {
            await _invoiceService.SendInvoice(new SendInvoiceRequest()
            {
                InvoiceId = invoiceId
            });
        }
    }
}