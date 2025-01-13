using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Models.Views;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Microsoft.Extensions.Configuration;

namespace API.Repositories.Data
{
    public class CourseRepository : GeneralRepository<MyContext, Course, int>
    {
        public readonly MyContext myContext;
        public IConfiguration _configuration;
        public CourseRepository(MyContext myContext , IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }
        public IEnumerable MasterCourse()
        {
            var masterData = (from usr in myContext.Users
                              join csr in myContext.Courses on usr.Id equals csr.UserId
                              join cnt in myContext.Contents on csr.Id equals cnt.CourseId
                              /*join ctl in myContext.Catalogues on csr.Id equals ctl.CourseId
                              join ctg in myContext.Categories on ctl.CategoryId equals ctg.Id*/
                              join scnt in myContext.SubContents on cnt.Id equals scnt.ContentId
                              select new
                              {
                                  FullName = usr.FirstName + " " + usr.LastName,
                                  Id = csr.Id,
                                  Title= csr.Title,
                                  Description = csr.Description,
                                  Price = csr.Price,
                                  Features = csr.Features,
                                  VideoName = scnt.VideoName,
                                  Duration = scnt.Duration,
                                  /*UserStatus = (from Course in myContext.AccountRoles
                                              join rl in myContext.Roles on account.RoleId equals rl.RoleId
                                              where account.AccountId == emp.NIK
                                              select new
                                              {
                                                  rl.RoleName
                                              }).Select(x => x.RoleName).ToArray()*/
                              }).ToList();

            return masterData;
        }
        public int UpdateStatus(Course course)
        {
            myContext.Entry(course).State = EntityState.Modified;
            myContext.Entry(course).Property(x => x.UserId).IsModified = false;
            myContext.Entry(course).Property(x => x.Title).IsModified = false;
            myContext.Entry(course).Property(x => x.Description).IsModified = false;
            myContext.Entry(course).Property(x => x.Price).IsModified = false;
            myContext.Entry(course).Property(x => x.Features).IsModified = false;
            myContext.Entry(course).Property(x => x.Picture).IsModified = false;
            var result = myContext.SaveChanges();
            return result;
        }
        public CourseMasterDataVM GetMasterData(int ID)
        {
            Course cse = myContext.Courses.Find(ID);
            CourseMasterDataVM masterData = new CourseMasterDataVM
            {
                Id = cse.Id,
                Title = cse.Title,
                Description = cse.Description,
                Price = cse.Price,
                Features = cse.Features,
                Picture = cse.Picture,
                Feedback = cse.Feedback,
            };

            if (cse.Status == Status.Incomplete)
            {
                masterData.Status = "Incomplete";
            }
            else if(cse.Status == Status.Review)
            {
                masterData.Status = "Review";
            }
            else if(cse.Status == Status.Revise)
            {
                masterData.Status = "Revise";
            }
            else
            {
                masterData.Status = "Approve";
            }

            masterData.User = myContext.Users.Find(cse.UserId);
            masterData.Categories = myContext.Categories.Where(cry => cry.Catalogues.Any(cge => cge.CourseId == ID)).ToList<Category>();
            masterData.Contents = myContext.Contents.Where(content => content.CourseId == ID).ToList<Content>();

            return masterData;
        }
        public IEnumerable<CourseMasterDataVM> GetMasterData()
        {
            List<CourseMasterDataVM> masterDataVMs = new List<CourseMasterDataVM> { };
            foreach (Course cse in myContext.Courses)
            {
                masterDataVMs.Add(this.GetMasterData(cse.Id));
            }
            return masterDataVMs;
        }
        public IEnumerable<Course> GetByUser(string ID)
        {
            return myContext.Courses.Where(content => content.UserId == ID).ToList<Course>();
        }
    }

    
}
