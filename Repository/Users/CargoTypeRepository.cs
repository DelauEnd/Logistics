﻿using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class CargoTypeRepository : RepositoryBase<CargoType>, ICargoTypeRepository
    {
        public CargoTypeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
