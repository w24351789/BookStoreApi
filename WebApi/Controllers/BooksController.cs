using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BookApp;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class BooksController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await Mediator.Send(new GetBooks.Query());
        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult<Book>> GetBook(int bookId)
        {
            return await Mediator.Send(new GetBook.Query { BookId = bookId});
        }

        [HttpGet("ISBN/{bookIsbn}")]
        public async Task<ActionResult<Book>> GetBook(string bookIsbn)
        {
            return await Mediator.Send(new GetBookByIsbn.Query { BookIsbn = bookIsbn});
        }

        [HttpGet("{bookId}/rating")]
        public async Task<ActionResult<decimal>> GetBookRating(int bookId)
        {
            return await Mediator.Send(new GetBookRating.Query { BookId = bookId });
          
        }
    }
}