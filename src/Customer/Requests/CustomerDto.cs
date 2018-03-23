﻿using System;

namespace Customer.Requests
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }
        public string Address { get; set; }
    }
}