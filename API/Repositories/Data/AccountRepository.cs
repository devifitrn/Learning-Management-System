using API.Context;
using API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using API.Models.Views;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public IConfiguration _configuration;

        private Random randomInt = new Random();
        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }

        public string Login(LoginVM loginVM)
        {
            var cekEmail = myContext.Users.SingleOrDefault(e => e.Email == loginVM.Email);
            if (cekEmail != null)
            {
                var cekPassword = myContext.Accounts.SingleOrDefault(e => e.Id == cekEmail.Id);
                if (BCrypt.Net.BCrypt.Verify(loginVM.Password, Convert.ToString(cekPassword.Password)))
                {
                    var getData = (from usr in myContext.Users
                                   join acc in myContext.Accounts on usr.Id equals acc.Id
                                   join auth in myContext.Authorities on acc.Id equals auth.AccountId
                                   join rl in myContext.Roles on auth.RoleId equals rl.Id
                                   where usr.Email == loginVM.Email
                                   select new
                                   {
                                       email = usr.Email,
                                       roles = rl.Name
                                   });
                    var claims = new List<Claim>();
                    claims.Add(new Claim("Email", loginVM.Email));
                    claims.Add(new Claim("Id", cekEmail.Id));
                    foreach (var item in getData)
                    {
                        claims.Add(new Claim("roles", item.roles));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddDays(1),
                                signingCredentials: signIn
                                );
                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                    return idtoken;
                }
                else
                {
                    return "1";
                }
            }
            else
            {
                return "2";
            }

            
        }
        public int Register(RegisterVM registerVM)
        {
            var usr = new User
            {
                Id = GenerateId(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                PhoneNumber = registerVM.PhoneNumber,
                BirthDate = registerVM.BirthDate,
                Email = registerVM.Email,
                Gender = (Gender)registerVM.Gender,
                ProfilePicture = registerVM.ProfilePicture,
            };
            myContext.Users.Add(usr);
            myContext.SaveChanges();
            var acc = new Account
            {
                Id = usr.Id,
                Password = Hashing.GenerateHashPassword(registerVM.Password),

            };
            myContext.Accounts.Add(acc);
            myContext.SaveChanges();
            var aut = new Authority
            {
                AccountId = acc.Id,
                RoleId = registerVM.Role
            };
            myContext.Authorities.Add(aut);
            return myContext.SaveChanges();
        }
        public User GetEmail(string email)
        {
            return myContext.Users.FirstOrDefault(x => x.Email == email);
        }
        public User GetEmail(string email, string id)
        {
            string oldEmail = myContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == id).Email;
            return myContext.Users.FirstOrDefault(x => x.Email != oldEmail && x.Email == email);
        }
        public User GetPhone(string phone)
        {
            return myContext.Users.FirstOrDefault(x => x.PhoneNumber == phone);
        }
        public User GetPhone(string phone, string id)
        {
            string oldPhone = myContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == id).PhoneNumber;
            return myContext.Users.FirstOrDefault(x => x.PhoneNumber != oldPhone && x.PhoneNumber == phone);
        }
        public string GenerateId()
        {
            string year = DateTime.Now.ToString("yyyy");
            var cari = myContext.Users.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id.StartsWith(year));
            if (cari == null)
            {
                return year + "001";
            }
            else
            {
                int id = Convert.ToInt32(cari.Id);
                id++;
                return Convert.ToString(id);
            }
        }
        public void GenerateOTP(Account account)
        {
            account.OTP = randomInt.Next(1000, 9999).ToString();
            account.IsUsed = false;
            DateTime currentDateTime = DateTime.Now;
            account.ExpiredToken = currentDateTime.AddMinutes(5);
            account.IsUsed = false;
            myContext.SaveChanges();
        }
        public int ResetPassword(string recipientEmail)
        {
            Account account;
            var usr = myContext.Users.SingleOrDefault(e => e.Email.ToLower() == recipientEmail.ToLower());
            if (usr == null)
            {
                return CheckConstants.EmailNotExists;
            }
            else
            {
                account = myContext.Accounts.Find(usr.Id);
                GenerateOTP(account);
            }
            string to = recipientEmail; //To address    
            string from = ""; //From address    
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = $"This email is used to send OTP for resetting account password.\nHere is your OTP: {account.OTP}";
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            NetworkCredential basicCredential1 = new
            NetworkCredential("test.msuryanto@gmail.com", "testaja321");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);
            return CheckConstants.Successful;
        }
        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var usr = myContext.Users.SingleOrDefault(e => e.Email.ToLower() == changePasswordVM.Email.ToLower());
            if (usr == null)
            {
                return CheckConstants.EmailNotExists;
            }
            else
            {
                var account = myContext.Accounts.Find(usr.Id);
                if (account.OTP == null) 
                {
                    return CheckConstants.OTPNotExists;
                }
                else if (account.OTP != changePasswordVM.OTP)
                {
                    return CheckConstants.WrongOTP;
                }
                else if (DateTime.Now >= account.ExpiredToken)
                {
                    return CheckConstants.OTPExpired;
                }
                else if (account.IsUsed == true)
                {
                    return CheckConstants.OTPIsUsed;
                }
                else if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
                {
                    return CheckConstants.InconsistentPassword;
                }
                else
                {
                    account.Password = Hashing.GenerateHashPassword(changePasswordVM.NewPassword);
                    account.IsUsed = true;
                    myContext.SaveChanges();
                    return CheckConstants.Successful;
                }
            }
        }

    }
}
