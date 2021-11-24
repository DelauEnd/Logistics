﻿using Entities.Models;
using Entities.RequestFeautures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class CargoRepositoryExtensions
    {
        public static IQueryable<Cargo> ApplyFilters(this IQueryable<Cargo> cargoes, CargoParameters parameters)
        {
            return cargoes.Where(cargo => 
                cargo.DepartureDate >= parameters.ArrivalDateFrom &&
                cargo.DepartureDate <= parameters.ArrivalDateTo &&
                cargo.DepartureDate >= parameters.DepartureDateFrom &&
                cargo.DepartureDate <= parameters.DepartureDateTo);
        }

        public static IQueryable<Cargo> Search(this IQueryable<Cargo> cargoes, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return cargoes;

            var searchValues = search.Trim().ToLower();

            return cargoes.Where(cargoes =>
                   cargoes.Title.Contains(searchValues) ||
                   cargoes.Type.Title.Contains(searchValues) ||
                   cargoes.Id.ToString().Contains(searchValues) ||
                   cargoes.Category.Title.Contains(searchValues));
        }
    }
}
