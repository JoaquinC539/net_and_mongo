using BookStoreApi.Bases;
using BookStoreApi.Models;
using BookStoreApi.Services;
using MongoDB.Driver;

namespace BookStoreApi.Controllers;
public class AuthorController:BaseApiControllerModel<AuthorService,Author>
{
    public AuthorController(AuthorService service) :base(service)
    {}
}