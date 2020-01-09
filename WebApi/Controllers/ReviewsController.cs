using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ReviewApp;
using Domain;
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
    }
}