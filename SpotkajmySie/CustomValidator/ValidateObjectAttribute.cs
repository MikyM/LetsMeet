using System;

namespace SpotkajmySie.CustomValidator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class ValidateObjectAttribute : Attribute
    {
        int _minOccurs = 0;

        public ValidateObjectAttribute() { }

        public int MinOccursOnEnumerable { get { return _minOccurs; } set { _minOccurs = value; } }

        public string ErrorMessage { get; set; }
    }
}