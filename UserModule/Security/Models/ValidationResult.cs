namespace UserModule.Validation;

public class ValidationResult
{
    public ValidationResult()
    {
        IsSuccess = true;
        IsFailure = false;
        Message = "";
    }

    public ValidationResult(string message)
    {
        IsSuccess = false;
        IsFailure = true;
        Message = message;
    }
    
    public bool IsSuccess { get; set; }
    public bool IsFailure { get; set; }
    public string Message  { get; set; }
}