using System;
using System.ComponentModel.DataAnnotations;

namespace PoznajmySie.CustomValidator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class TimeSpanAttribute : ValidationAttribute
    {
        public TimeSpanAttribute() { }

        public override bool IsValid(object value)
        {
            string strValue = value as string;
            if (!string.IsNullOrEmpty(strValue))
            {
                return TimeSpan.TryParse(strValue, out TimeSpan result);
            }
            return true;
        }
    }
}
