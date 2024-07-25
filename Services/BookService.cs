namespace BookStoreApi.Services;

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Amazon.Runtime;
using BookStoreApi.Bases;
using BookStoreApi.Interfaces;
using BookStoreApi.Models;
using BookStoreApi.Models.Book;
using DnsClient.Protocol;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

public class BookService : BaseApiService<Book>
{
    IMongoCollection<Author> _authorCollection;
    IMongoCollection<Category> _categoryCollection;



    public BookService(IMongoDatabase database) : base(database, "Books")
    {

        _authorCollection = database.GetCollection<Author>("Authors");
        _categoryCollection = database.GetCollection<Category>("Categories");
    }

    public async override Task<ICollection<Book>> GetAllAsync()
    {
        ICollection<Book> books = await mainCollection.Find(_ => true).ToListAsync();
        var authorIds = books.Select(b => b.AuthorId).Where(id => id != null).Distinct().ToList();
        var categoryIds = books.SelectMany(b => b.CategoryIds).Where(id => id != null).Distinct().ToList();

        var authors = await _authorCollection.Find(a => authorIds.Contains(a.Id!)).ToListAsync();
        var categories = await _categoryCollection.Find(c => categoryIds.Contains(c.Id!)).ToListAsync();
        foreach (var book in books)
        {
            book.Author = authors.FirstOrDefault(a => a.Id == book.AuthorId);
            book.Categories = categories.Where(c => categoryIds.Contains(c.Id!)).ToList();

        }
        return books;
    }
    public async override Task<Book?> GetFromIdAsync(string id)
    {
        var book = await mainCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        if (book != null)
        {
            var author = await _authorCollection.Find(a => a.Id == book.AuthorId).FirstOrDefaultAsync();
            var categories = await _categoryCollection.Find(c => book.CategoryIds.Contains(c.Id!)).ToListAsync();
            book.SetReferences(author, categories);
        }


        return book;
    }

    public override async Task<Book?> CreateAsync(Book book)
    {
        var author = await _authorCollection.Find(a => a.Id == book.AuthorId).FirstOrDefaultAsync();
        if (author == null)
        {
            return null;
        }

        book.AuthorId = author.Id!;
        var categories = await _categoryCollection.Find(c => book.CategoryIds.Contains(c.Id!)).ToListAsync();
        book.CategoryIds = categories.Select(c => c.Id!).ToList();
        await mainCollection.InsertOneAsync(book);
        return book;
    }

    public override async Task<Book?> UpdateAsync(Book book,string id)
    {
        var entity =await mainCollection.Find(e=>e.Id==id).FirstOrDefaultAsync();
        if(entity==null)
        {
            return default;
        }
        book.Id= entity.Id;
        FilterDefinition<Book> filter=Builders<Book>.Filter.Eq(b=>b.Id,id);
        var author = await _authorCollection.Find(a => a.Id == book.AuthorId).FirstOrDefaultAsync();
        if (author == null)
        {
            return null;
        }

        book.AuthorId = author.Id!;
        var categories = await _categoryCollection.Find(c => book.CategoryIds.Contains(c.Id!)).ToListAsync();
        book.CategoryIds = categories.Select(c => c.Id!).ToList();
        await mainCollection.ReplaceOneAsync(filter,book);
        return book;
    }
    public async Task<ICollection<Dictionary<string, object>>> GetAllByAggregate()
    {
        var pipeline = new List<BsonDocument>();
        pipeline.Add(new BsonDocument("$lookup", new BsonDocument{
            {"from","Authors"},
            {"localField","AuthorId"},
            {"foreignField","_id"},
            {"as","Author"}
        }));
        pipeline.Add(new BsonDocument("$lookup", new BsonDocument{
            {"from","Categories"},
            {"localField","CategoryIds"},
            {"foreignField","_id"},
            {"as","Categories"}
        }));
        pipeline.Add(new BsonDocument{
            {"$unwind",new BsonDocument{
                {"path","$Author"},
                {"preserveNullAndEmptyArrays",false}
            }}
        });
        pipeline.Add(new BsonDocument(
            "$addFields", new BsonDocument{
                {"BasePrice", new BsonDocument("$toDouble", "$Price")},
                {"Tax",new BsonDocument(
                    "$multiply",new BsonArray{new BsonDocument("$toDouble","$Price"),0.1}
                )}
                
                
                }
                ));
        pipeline.Add(new BsonDocument(
            "$addFields",new BsonDocument(
                "FinalPrice",new BsonDocument("$add",new BsonArray{"$Tax","$BasePrice"})
            )
        ));
        pipeline.Add(new BsonDocument{
            {"$project",new BsonDocument{
                {"_id",1},
                {"BasePrice",1},
                {"Tax",1},
                {"FinalPrice",1},
                {"Author","$Author.Name"},
                {"Categories","$Categories.Name"}                
            }}
        });

        var bsonDocuments = await mainCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();
        var results = bsonDocuments.Select(doc => doc.ToDictionary(
           kvp => kvp.Name,
           kvp => BsonTypeMapper.MapToDotNetValue(kvp.Value)
        )).ToList();




        return results;

    }




}