using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CheckValidationName : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                var len = value.ToString().Length;
                if (len > 3 && len < 50)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CheckValidationAge : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int)
            {
                var Data = (int)value;
                if (Data > 0 && Data < 71)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CheckValidationNationalCode : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var a = (string)value;
                if (a.Length == 10)
                {
                    var data = double.Parse(a);
                    if (data > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
