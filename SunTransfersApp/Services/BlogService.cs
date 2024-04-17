using SunTransfersApp.Logic;
using SunTransfersApp.Models;
using SunTransfersApp.Validations;

namespace SunTransfersApp.Services
{
    public class ClientsService 
    {
        private readonly ClientsLogic _logic;
        private readonly ClientsValidations _validations;
        public ClientsService(ClientsLogic clientsLogic, ClientsValidations clientsValidations)
        {
            _logic = clientsLogic;
            _validations = clientsValidations;
        }

        public async Task<object> GetAllClients()
        {

            try
            {
                //Get List Clients
                var clients = await _logic.GetAllClients();

                return Results.Ok(clients);

            }
            catch (Exception )
            {
                //TODO: Create Logs
                return Results.BadRequest(new ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error"));
            }
        }

        public async Task<object> GetClientById(Guid clientId)
        {
            
            try
            {
                //Model Validation
                if (clientId == Guid.Empty)
                {
                    return Results.BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "wrong format ClientId"));
                }
                //Get Client
                var client = await _logic.GetClientById(clientId);

                //Validate Client Exist
                if (client != null)
                    return Results.Ok(client);

                //Client Not Found Response
                return Results.NotFound(new ErrorResponse(StatusCodes.Status404NotFound, "Client Not found"));
            }
            catch (Exception)
            {
                //TODO: Create Logs
                return Results.BadRequest(new ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error"));
            }
        }

        public async Task<object> CreateClient(Client client)
        {

            try
            {
                //Model Validation
                var validation = await _validations.ClientValidations(client);
                if (validation.status != StatusCodes.Status200OK)
                {
                    return validation;
                }

                //Create Client

                var create = await _logic.CreateClient(client);
                if (create.status == StatusCodes.Status201Created)
                    return Results.Created("/getClientById/", client);

                return create;
            }
            catch (Exception)
            {
                //TODO: Create Logs
                return Results.BadRequest(new ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error"));
            }
        }

        public async Task<object> UpdateClient(Guid clientId, Client client)
        {

            try
            {
                if (clientId != client.clientId)
                {
                    return Results.BadRequest("Path client Id and Body client id are distinct");
                }
                //Model Validation
                var validation = await _validations.ClientValidations(client);
                if (validation.status != StatusCodes.Status200OK)
                {
                    return validation;
                }

                //Update Client
                var create = await _logic.UpdateClient(client);
                if (create.status == StatusCodes.Status200OK)
                    return Results.Ok(client);

                return create;
            }
            catch (Exception)
            {
                //TODO: Create Logs
                return Results.BadRequest(new ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error"));
            }
        }
        public async Task<object> DeleteClient(Guid clientId)
        {

            try
            {
                //Delete Client
                var create = await _logic.DeleteClient(clientId);
                if (create.status == StatusCodes.Status200OK)
                    return Results.NoContent();

                return create;
            }
            catch (Exception)
            {
                //TODO: Create Logs
                return Results.BadRequest(new ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error"));
            }
        }
    }
}
