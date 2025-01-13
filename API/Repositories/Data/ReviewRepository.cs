using API.Context;
using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class ReviewRepository : GeneralRepository<MyContext, Review, int>
    {
        private readonly MyContext myContext;
        public ReviewRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public Review CheckReview(string userId, int courseId)
        {
            return myContext.Reviews.Where(review => review.UserId == userId).Where(review => review.CourseId == courseId).FirstOrDefault();

        }

        public IEnumerable<Review> GetCourseReviews(int courseId)
        {
            return myContext.Reviews.Where(review => review.CourseId == courseId).OrderByDescending(review => review.Id).ToList();
        }


        public IEnumerable<Review> GetByCourse(int courseId)
        {
            return myContext.Reviews.Where(review => review.CourseId == courseId).ToList();
        }
    }
}
