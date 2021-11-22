using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Validators
{
    public interface ICargoCategoryValidator : IValidator<CargoCategory>
    {
        public bool TitleIsValid(string title);
    }
}
