namespace BookStoreApi.Controllers;

using BookStoreApi.Bases;
using BookStoreApi.Models.Book;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

public class BookController : BaseApiControllerModel<BookService,Book>
{

  public BookController(BookService bookService) : base(bookService)
  {

  }
  [HttpGet]
  [Route("agg")]
  public  async Task<ActionResult<ICollection<Dictionary<string,object>>>> GetBooksAggre()
  {
    var booksBson=await service.GetAllByAggregate();
    
    
    return Ok(booksBson);
  }
  

}