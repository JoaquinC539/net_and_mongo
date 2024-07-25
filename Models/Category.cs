using System.Diagnostics.CodeAnalysis;
using BookStoreApi.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreApi.Models;
public class Category(string categoryName) : IEntity
{
    
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]    
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string CategoryName { get; set; } = categoryName;
}