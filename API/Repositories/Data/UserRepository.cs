using API.Context;
using API.Models;
using API.Models.Views;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace API.Repositories.Data
{
    public class UserRepository : GeneralRepository<MyContext, User, string>
    {
        private readonly MyContext myContext;
        public UserRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public UserMasterDataVM GetMasterData(string ID)
        {
            User usr = myContext.Users.Find(ID);
            UserMasterDataVM masterData = new UserMasterDataVM
            {
                Id = usr.Id,
                FullName = usr.FirstName + " " + usr.LastName,
                BirthDate = usr.BirthDate,
                Email = usr.Email,
                PhoneNumber = usr.PhoneNumber,
                Status = usr.Status,

            };

            if (usr.Gender == Gender.Male)
            {
                masterData.Gender = "Laki-laki";
            }
            else
            {
                masterData.Gender = "Perempuan";
            }

            masterData.Roles = "";
            var roles = myContext.Roles.Where(role => role.Authorities.Any(aty => aty.AccountId == ID)).ToList<Role>();
            for (int i = 0; i < roles.Count; i++)
            {
                if (i == roles.Count - 1)
                {
                    masterData.Roles += roles[i].Name;
                }
                else
                {
                    masterData.Roles += roles[i].Name + ", ";
                }
            }

            return masterData;
        }
        public IEnumerable<UserMasterDataVM> GetMasterData()
        {
            List<UserMasterDataVM> masterDataVMs = new List<UserMasterDataVM> { };
            foreach (User usr in myContext.Users)
            {
                masterDataVMs.Add(this.GetMasterData(usr.Id));
            }
            return masterDataVMs;
        }
        public IEnumerable<User> GetStudentData()
        {
            List<User> students = new List<User> { };
            foreach (User usr in myContext.Users)
            {
                if (usr.Account.Authorities.All(aty => aty.RoleId == 3))
                {
                    students.Add(this.Get(usr.Id));
                }   
            }
            return students;
        }
        public int UpdateStatus(User user)
        {
            myContext.Entry(user).State = EntityState.Modified;
            myContext.Entry(user).Property(x => x.Id).IsModified = false;
            myContext.Entry(user).Property(x => x.FirstName).IsModified = false;
            myContext.Entry(user).Property(x => x.LastName).IsModified = false;
            myContext.Entry(user).Property(x => x.BirthDate).IsModified = false;
            myContext.Entry(user).Property(x => x.ProfilePicture).IsModified = false;
            myContext.Entry(user).Property(x => x.PhoneNumber).IsModified = false;
            myContext.Entry(user).Property(x => x.Email).IsModified = false;
            myContext.Entry(user).Property(x => x.Gender).IsModified = false;
            var result = myContext.SaveChanges();
            return result;
        }




    }
}
