﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CreatePaymentDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
    }
}
