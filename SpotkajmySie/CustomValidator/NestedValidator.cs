using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;

namespace SpotkajmySie.CustomValidator
{
    public static class NestedValidator
    {
        public static bool TryValidateObject(object obj, ICollection<ValidationResult> results)
        {
            return Validator.TryValidateObject(obj, new ValidationContext(obj, null, null), results, true);
        }

        public static bool TryValidateObjectRecursive<T>(T obj, List<ValidationResult> results)
        {
            bool result = TryValidateObject(obj, results);

            var properties = obj.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ValidateObjectAttribute)));

            foreach (var property in properties) {
                var valAttrib = property.GetCustomAttributes(typeof(ValidateObjectAttribute), true).FirstOrDefault() as ValidateObjectAttribute;
                var value = obj.GetPropertyValue(property.Name);

                if (value == null || valAttrib == null) continue;

                var asEnumerable = value as IEnumerable;
                if (asEnumerable != null) {
                    List<object> items = new List<object>();
                    foreach (var enumObj in asEnumerable) items.Add(enumObj);
                    foreach (var enumObj in items) {
                        result = TryValidateObjectRecursive(enumObj, results) && result;
                    }
                    if (items.Count < valAttrib.MinOccursOnEnumerable) {
                        string errorMessage = valAttrib.ErrorMessage ?? "MinOccursOnEnumerable validation failed.";
                        results.Add(new ValidationResult(errorMessage));
                        result = false;
                    }
                } else {
                    result = TryValidateObjectRecursive(value, results) && result;
                }
            }

            return result;
        }
    }

    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object o, string propertyName)
        {
            object objValue = string.Empty;

            var propertyInfo = o.GetType().GetProperty(propertyName);
            if (propertyInfo != null) {
                objValue = propertyInfo.GetValue(o, null);
            }
            return objValue;
        }
    }
}