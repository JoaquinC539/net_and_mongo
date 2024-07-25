using System.Text.Json.Serialization;
using BookStoreApi.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
namespace BookStoreApi.Models.Book;



public class Book : IEntity
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

    public string? Id { get; set; }

    [BsonElement("Name")]
    public string BookName { get; set; }

    public decimal Price { get; set; }

    [BsonElement("CategoryIds")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ICollection<string> CategoryIds { get; set; }

    [BsonElement("AuthorId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string AuthorId { get; set; }

    [BsonIgnore]    
    public Author? Author { get; set; }

    [BsonIgnore]
        
    public ICollection<Category>? Categories { get; set; }

    
    
    public Book(string bookName,decimal price,string authorId,ICollection<string> categoryIds)
    {
        BookName=bookName;
        Price=price;
        AuthorId=authorId;
        CategoryIds=categoryIds;
        Author=null;
        Categories=null;       
    }
    public void SetReferences(Author author, ICollection<Category> categories)
    {
        Author = author;
        Categories = categories;
    }
}