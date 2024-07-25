
using BookStoreApi.Interfaces;

namespace BookStoreApi.Models.Book;



public class BookDto : IEntityDto
{
    
    public string BookName { get; set; }
    public decimal Price { get; set; }
    public string AuthorId { get; set; }
    public ICollection<string> CategoryIds { get; set; }
    
    public BookDto(string bookName,decimal price,string authorId,ICollection<string> categoryIds)
    {
        BookName=bookName;
        Price=price;
        AuthorId=authorId;
        CategoryIds=categoryIds;
    }
}