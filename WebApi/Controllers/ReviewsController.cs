using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ReviewApp;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    public class ReviewsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetReviews()
        {
            return await Mediator.Send(new GetReviews.Query());
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<Review>> GetReview(int reviewId)
        {
            return await Mediator.Send(new GetReview.Query { ReviewId = reviewId });
        }

        [HttpGet("books/{bookId}")]
        public async Task<ActionResult<List<Review>>> GetReviewsForABook(int bookId)
        {
            return await Mediator.Send(new GetReviewsForABook.Query { BookId = bookId });
        }

        [HttpGet("{reviewId}/book")]
        public async Task<ActionResult<Book>> GetBookForAReview(int reviewId)
        {
            return await Mediator.Send(new GetBookForAReview.Query { ReviewId = reviewId });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateReview(CreateReview.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{reviewId}")]
        public async  Task<ActionResult<Unit>> EditReview(int reviewId, EditReview.Command command)
        {
            command.Id = reviewId;
            return await Mediator.Send(command);
        }
    }
}