namespace UserModule.Validation;

public static class ValidationResultFactory
{
    public static ValidationResult CreateSuccessResult()
    {
        return new ValidationResult();
    }

    public static ValidationResult CreateFailedResult(string message)
    {
        return new ValidationResult(message);
    }
}