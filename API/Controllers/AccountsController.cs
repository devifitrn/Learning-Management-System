using API.Base;
using API.Models;
using API.Models.Views;
using API.Repositories;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            string login = accountRepository.Login(loginVM);
            return login switch
            {
                "1" => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Login Failed (Password salah)" }),
                "2" => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Login Failed (Email dan password salah)" }),
                _ => Ok(new { status = HttpStatusCode.OK, JWT = login, message = "Login Successfull" })
                // _ => NotFound(new { status = HttpStatusCode.NotFound, message = "error" })
            };
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            //accountRepository.Register(registerVM);
            //return Ok(accountRepository.Register(registerVM));
            if (accountRepository.GetEmail(registerVM.Email) != null)
            {
                return BadRequest(new { status = 400, message = "Email sudah Terdaftar" });
            }
            if (accountRepository.GetPhone(registerVM.PhoneNumber) != null)
            {
                return BadRequest(new { status = 400, message = "Phone Number sudah Terdaftar" });
            }
            accountRepository.Register(registerVM);
            return Ok(new { status = 200, message = "Data Berhasil Diupdate" });
        }
        [HttpPost("resetpassword")]
        public ActionResult ResetPaswword(ResetPasswordVM vm)
        {
            try
            {
                if (accountRepository.ResetPassword(vm.Email) == CheckConstants.Successful)
                {
                    return Ok(new { Status = 200, Message = "OTP sent" });
                }
                else
                {
                    return NotFound(new { Status = 404, Message = "Email not found in the database"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [HttpPost("changepassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            try
            {
                var result = accountRepository.ChangePassword(changePasswordVM);
                switch (result)
                {
                    case CheckConstants.Successful:
                        return Ok(new { Status = 200, Message = $"The password of {changePasswordVM.Email} account has changed" });
                    case CheckConstants.EmailNotExists:
                        return NotFound(new { Status = 404, Message = $"Wrong email" });
                    case CheckConstants.WrongOTP:
                        return BadRequest(new { Status = 400, Message = "Wrong OTP" });
                    case CheckConstants.OTPExpired:
                        return BadRequest(new { Status = 400, Message = "Expired OTP" });
                    case CheckConstants.OTPIsUsed:
                        return BadRequest(new { Status = 400, Message = "OTP has been used for a password change once before" });
                    case CheckConstants.InconsistentPassword:
                        return BadRequest(new { Status = 400, Message = "The new password wasn't confirmed" });
                    default:
                        return null;
                }                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }
    }
}
