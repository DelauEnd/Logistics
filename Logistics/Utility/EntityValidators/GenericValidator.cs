using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.EntityValidators
{
    public class GenericValidator
    {
        public bool IsPositiveNumber(int number)
            => number >= 0;

        public bool IsStringCorrect(string str)
            => !string.IsNullOrWhiteSpace(str);
    }
}
