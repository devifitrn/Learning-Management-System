using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController<Review, ReviewRepository, int>
    {
        private readonly ReviewRepository ReviewRepository;
        public ReviewsController(ReviewRepository ReviewRepository) : base(ReviewRepository)
        {
            this.ReviewRepository = ReviewRepository;
        }

        [HttpGet("CheckReview/{userId}/{courseId}")]
        public virtual ActionResult CheckReview(string userId, int courseId)
        {
            var result = ReviewRepository.CheckReview(userId, courseId);
            return Ok(result);
        }

        [HttpGet("GetCourseReviews/{courseId}")]
        public ActionResult GetReview(int courseId)
        {
            var result = ReviewRepository.GetCourseReviews(courseId);
            return Ok(result);
        }
        [HttpGet("AvgRating/{courseId}")]
        public virtual ActionResult AvgRating (int courseId)
        {
            var result = ReviewRepository.GetByCourse(courseId);
            int jumlah = result.Count();
            if (jumlah == 0)
            {
                return Ok(new { review = 0, rating = 0});
            }
            else
            {
                double totRating = 0;
                foreach (var item in result)
                {
                    totRating += item.Rating;
                }
                double avgRating = Math.Round((totRating / jumlah), 2);
                return Ok(new { review = jumlah, rating = avgRating});
            }
        }
    }
}
