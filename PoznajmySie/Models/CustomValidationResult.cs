using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CustomValidationResult : ValidationResult
{
    public CustomValidationResult() : base("")
    {

    }
    public IList<ValidationResult> NestedResults { get; set; }
}