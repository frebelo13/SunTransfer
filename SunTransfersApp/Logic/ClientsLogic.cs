using SunTransfersApp.Models;

namespace SunTransfersApp.Logic
{
    public class ClientsLogic : BaseLogic
    {
        private readonly string _clientFile = "clients.json";
        public ClientsLogic(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Client> GetClientById(Guid clientId)
        {
            try
            {
                var clients = await GetInFileAsync<List<Client>>(_clientFile);
                var client = clients.Where(x=>x.clientId == clientId).FirstOrDefault();
                return client;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<Client>> GetAllClients()
        {
            try
            {
                return await GetInFileAsync<List<Client>>(_clientFile);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ErrorResponse> CreateClient(Client client)
        {
            try
            {
                var clients = await GetAllClients();
                if (clients == null)
                    clients = new List<Client>();

                if(clients.Where(x => x.clientId == client.clientId).Count() > 0)
                {
                    return new ErrorResponse(StatusCodes.Status400BadRequest, "Already exist a client with this id");
                }
                if (clients.Where(x => x.email == client.email).Count() > 0)
                {
                    return new ErrorResponse(StatusCodes.Status400BadRequest, "Already exist a client with this email");
                }

                clients.Add(client);

                var createResult = await UpdateInFileAsync(_clientFile, clients);

                if (createResult)
                    return new ErrorResponse(StatusCodes.Status201Created);

                return new ErrorResponse(StatusCodes.Status400BadRequest, "Error creating client");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<ErrorResponse> UpdateClient(Client client)
        {
            try
            {
                var clients = await GetAllClients();
                if (clients == null)
                    clients = new List<Client>();

                if (clients.Where(x => x.clientId == client.clientId).Count() != 1)
                {
                    return new ErrorResponse(StatusCodes.Status400BadRequest, "Client Not Found With This Id");
                }
                if (clients.Where(x => x.clientId != client.clientId && x.email == client.email).Count() > 0)
                {
                    return new ErrorResponse(StatusCodes.Status400BadRequest, "Already exist a client with this email");
                }


                var clientIndex = clients.FindIndex(x => x.clientId == client.clientId);
                clients[clientIndex] = client;

                var createResult = await UpdateInFileAsync(_clientFile, clients);

                if (createResult)
                    return new ErrorResponse(StatusCodes.Status200OK);

                return new ErrorResponse(StatusCodes.Status400BadRequest, "Error creating client");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<ErrorResponse> DeleteClient(Guid clientId)
        {
            try
            {
                var clients = await GetAllClients();
                if (clients == null)
                    clients = new List<Client>();

                if (clients.Where(x => x.clientId == clientId).Count() != 1)
                {
                    return new ErrorResponse(StatusCodes.Status400BadRequest, "Client Not Found With This Id");
                }


                var client = clients.Find(x => x.clientId == clientId);
                clients.Remove(client);

                var createResult = await UpdateInFileAsync(_clientFile, clients);

                if (createResult)
                    return new ErrorResponse(StatusCodes.Status200OK);

                return new ErrorResponse(StatusCodes.Status400BadRequest, "Error creating client");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
