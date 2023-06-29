using System.ComponentModel.DataAnnotations;

namespace SEP.CustomValidation
{
    public class StartAndEndDateValidator : ValidationAttribute
    {
        private readonly DateTime _startDate;
        public StartAndEndDateValidator(DateTime startDate) 
        {
            _startDate = startDate;
            const string defaultErrorMessage = "End Date cannot be before start date";
            ErrorMessage ??= defaultErrorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var endDate = ((DateTime)value!);
            if (endDate < _startDate)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
