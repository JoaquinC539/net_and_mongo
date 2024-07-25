using BookStoreApi.Bases;
using BookStoreApi.Models;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class CategoryService:BaseApiService<Category>
{
    public CategoryService(IMongoDatabase database):base(database,"Categories")
    {}
}