using Microsoft.AspNetCore.Mvc;
using SunTransfersApp.Infraestructure.Interfaces;
using SunTransfersApp.Models;
using SunTransfersApp.Services;

namespace SunTransfersApp.Infraestructure
{
    public class Endpoints : IEndpoints
    {
        public void ClientsEndpoints(WebApplication app)
        {
            //By defaul not necesary define Api version if it's 1.0
            app.MapGet("client/GetAllClients", async (ClientsService service) =>
                await service.GetAllClients()
            ).Produces<List<Client>>(StatusCodes.Status200OK)
            .WithName("Get all clients").WithTags("Clients");

            app.MapGet("client/GetClientById/{id}", async (ClientsService service, Guid id) =>
                await service.GetClientById(id)
            ).Produces<Client>(StatusCodes.Status200OK)
            .WithName("Get Client By Id").WithTags("Clients");

            app.MapPost("client/CreateClient", async (ClientsService service, Client request) =>
                await service.CreateClient(request)
            ).Produces<Client>(StatusCodes.Status201Created)
            .WithName("Create new Client").WithTags("Clients");

            app.MapPut("client/UpdateClient/{id}", async (ClientsService service, Guid id, [FromBody]Client request) =>
                await service.UpdateClient(id,request)
            ).Produces<Client>(StatusCodes.Status200OK)
            .WithName("Update Client").WithTags("Clients");

            app.MapDelete("client/DeleteClient/{id}", async (ClientsService service, Guid id) =>
                await service.DeleteClient(id)
            ).Produces(StatusCodes.Status204NoContent)
            .WithName("Delete Client").WithTags("Clients");

        }
        
    }
}
