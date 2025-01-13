using API.Context;
using API.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class EnrollmentRepository : GeneralRepository<MyContext, Enrollment, int>
    {
        private readonly MyContext myContext;
        public EnrollmentRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public ICollection <Enrollment> GetEnrollment(string userId)
        {
            return myContext.Enrollments.Where(enrollment => enrollment.UserId == userId).ToList<Enrollment>();
        }
        public Enrollment EnrollMid(Enrollment enrollment)
        {
            Enrollment enroll = new Enrollment
            {
                UserId = enrollment.UserId,
                CourseId = enrollment.CourseId,
                Status = enrollment.Status,
                StartDate = enrollment.StartDate
            };
            myContext.Add(enroll);
            myContext.SaveChanges();
            return enroll;
        }
        public Enrollment CheckEnrollment(string userId, int courseId)
        {
            return myContext.Enrollments.Where(enrollment => enrollment.UserId == userId).Where(enrollment => enrollment.CourseId == courseId).FirstOrDefault();

        }
    }
}
