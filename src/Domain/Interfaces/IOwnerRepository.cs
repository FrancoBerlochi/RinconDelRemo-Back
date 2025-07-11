﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOwnerRepository : IBaseRepository<Owner, string>
    {
        Owner? GetByNameLastname(string name, string lastname);
        Owner GetByOwnerEmail(string email);
    }
}
