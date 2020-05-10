﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace IdentitySystem.API.Helpers
{
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}
