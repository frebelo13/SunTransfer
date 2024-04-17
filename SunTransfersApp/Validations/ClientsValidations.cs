using SunTransfersApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SunTransfersApp.Validations
{
    public class ClientsValidations
    {
        public async Task<ErrorResponse> ClientValidations(Client client)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(client, null, null);
            var isValid = Validator.TryValidateObject(client, context, results, true);

            if (isValid)
            {
                return new ErrorResponse(StatusCodes.Status200OK);
            }

            return new ErrorResponse (StatusCodes.Status400BadRequest, results.Select(r => new ValidationError(r.MemberNames.FirstOrDefault(), r.ErrorMessage)).ToList());
           
        }


    }
}
