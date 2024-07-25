using BookStoreApi.Bases;
using BookStoreApi.Interfaces;
using BookStoreApi.Models;
using MongoDB.Driver;

namespace BookStoreApi.Services;
public class AuthorService : BaseApiService<Author>
{
    public AuthorService(IMongoDatabase database):base(database,"Authors")
    {}
    
}