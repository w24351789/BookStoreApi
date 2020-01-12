using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ReviewerApp;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ReviewersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Reviewer>>> GetReviewers()
        {
            return await Mediator.Send(new GetReviewers.Query());
        }

        [HttpGet("{reviewerId}")]
        public async Task<ActionResult<Reviewer>> GetReviewerId(int reviewerId)
        {
            return await Mediator.Send(new GetReviewer.Query { ReviewerId = reviewerId });
        }

        [HttpGet("{reviewerId}/reviews")]
        public async Task<ActionResult<List<Review>>> GetReviewsByReviewer(int reviewerId)
        {
            return await Mediator.Send(new GetReviewsByReviewer.Query { ReviewerId = reviewerId });
        }

        [HttpGet("{reviewId}/reviewer")]
        public async Task<ActionResult<Reviewer>> GetReviewerOfAReview(int reviewId)
        {
            return await Mediator.Send(new GetReviewerOfAReview.Query { ReviewId = reviewId });
        }
    }
}