using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Validators
{
    public interface IValidator<T>
    {
        public bool IsValid(T entity);
    }
}
