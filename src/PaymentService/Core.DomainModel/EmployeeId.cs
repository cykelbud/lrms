﻿using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace PaymentService.Core.DomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class EmployeeId : Identity<EmployeeId>
    {
        public EmployeeId(string value) : base(value)
        {
        }
    }
}