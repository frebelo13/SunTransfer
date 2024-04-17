namespace SunTransfersApp.Models
{
    public class ErrorResponse
    {
        public ErrorResponse() { }
        public ErrorResponse(int status)
        {
            this.status = status;
        }
        public ErrorResponse(int status, string message) {
            this.status = status;
            this.message = new List<string> { message };
        }
        public ErrorResponse(int status, List<string> message)
        {
            this.status = status;
            this.message =  message;
        }
        public ErrorResponse(int status, List<ValidationError> validationErrors)
        {
            this.status = status;
            this.validationErrors = validationErrors;
        }
        public int status { get; set; }
        public List<string> message { get; set; }
        public List<ValidationError> validationErrors { get; set; }

    }
    public class ValidationError
    {
        public ValidationError() { }
        public ValidationError(string field, string message) {
            this.field = field;
            this.message = message;
        }
        public string field { get; set; }
        public string message { get; set; }
    }
}
