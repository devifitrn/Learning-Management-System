using Client.Base;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Client.Repositories.Data;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class ReviewsController : BaseController<Review, ReviewRepository, int>
    {
        private readonly ReviewRepository repository;
        public ReviewsController(ReviewRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpPost("PostReview")]
        public JsonResult PostInstructorRole(Review review)
        {
            var result = repository.Post(review);
            return Json(result);
        }
        
        [HttpGet("GetCourseReviews/{courseId}")]
        public async Task<JsonResult> GetReview(int courseId)
        {
            var result = await repository.GetCourseReviews(courseId);
            return Json(result);
        }
        [HttpGet("AvgRating/{id}")]
        public async Task<JsonResult> AvgRating(int id)
        {
            var result = await repository.AvgRating(id);
            return Json(result);
        }
        [HttpGet("CheckReview/{userId}/{id}")]
        public async Task<JsonResult> CheckReview(string userId, int id)
        {
            var result = await repository.CheckReview(userId, id);
            return Json(result);
        }
    }
}
