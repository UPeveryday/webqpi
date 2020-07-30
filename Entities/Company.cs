﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public string Introduction { get; set; }

        public ICollection<Emloyee> Emloyees { get; set; }
    }
}