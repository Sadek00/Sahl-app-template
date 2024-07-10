using FluentValidation.Results;

namespace AppTemplate.Dto.Helpers
{
    public static class FluentValidationHelper
    {
        public static List<string> GetErrorMessage(List<ValidationFailure> errors)
        {
            List<string> errorsMessages = new List<string>();
            foreach (var failure in errors)
            {
                errorsMessages.Add(failure.ErrorMessage);
            }
            return errorsMessages;
        }
    }
}
