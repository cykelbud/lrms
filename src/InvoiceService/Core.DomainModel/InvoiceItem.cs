﻿using EventFlow.ValueObjects;

namespace InvoiceService.Core.DomainModel
{
    public class InvoiceItem : ValueObject
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}