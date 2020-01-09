using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.AuthorApp;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AuthorsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            return await Mediator.Send(new GetAuthors.Query());
        }

        [HttpGet("{authorId}")]
        public async Task<ActionResult<Author>> GetAuthor(int authorId)
        {
            return await Mediator.Send(new GetAuthor.Query { AuthorId = authorId });
        }

        [HttpGet("books/{bookId}")]
        public async Task<ActionResult<Author>> GetAuthorOfABook(int bookId)
        {
            return await Mediator.Send(new GetAuthorOfABook.Query { BookId = bookId });
        }

        [HttpGet("{authorId}/books")]
        public async Task<ActionResult<List<Book>>> GetBooksOfAnAuthor(int authorId)
        {
            return await Mediator.Send(new GetBooksOfAnAuthor.Query { AuthorId = authorId });
        }
    }
}