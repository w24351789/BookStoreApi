using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CategoryApp;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await Mediator.Send(new GetCategories.Query());
        }
       
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Category>> GetCategory(int categoryId)
        {
            return await Mediator.Send(new GetCategory.Query { CategoryId = categoryId });
        }
        
        [HttpGet("books/{bookId}")]
        public async Task<ActionResult<List<Category>>> GetAllCategoriesOfABook(int bookId)
        {
            return await Mediator.Send(new GetAllCategoriesOfABook.Query { BookId = bookId });
        }
        
        [HttpGet("{categoryId}/books")]
        public async Task<ActionResult<List<Book>>> GetAllBookForCategory(int categoryId)
        {
            return await Mediator.Send(new GetAllBooksForCategory.Query { CategoryId = categoryId });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCategory(CreateCategory.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult<Unit>> EditCategory(int categoryId, EditCategory.Command command)
        {
            command.CategoryId = categoryId;
            return await Mediator.Send(command);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<Unit>> DeleteCategory(int categoryId)
        {
            return await Mediator.Send(new DeleteCategory.Command { CategoryId = categoryId });
        }
    }
}