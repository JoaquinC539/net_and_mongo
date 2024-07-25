using System.Diagnostics.CodeAnalysis;
using BookStoreApi.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreApi.Models;
public class Author(string authorName) : IEntity
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string AuthorName { get; set; } = authorName;
}