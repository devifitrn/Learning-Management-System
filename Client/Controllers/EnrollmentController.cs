using API.Models;
using Client.Base;
using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class EnrollmentController : BaseController<Enrollment, EnrollmentRepository, int>
    {
        private readonly EnrollmentRepository repository;
        public EnrollmentController(EnrollmentRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult Enroll(Enrollment enrollment)
        {
            var result = repository.GetEnrollment(enrollment);
            return Json(result);
        }

       /* [HttpGet]
        public JsonResult GetEnrollment(string UserId)
        {
            var result = repository.GetEnrollData(UserId);
            return Json(result);
        }*/

        [HttpGet("getenrollmentdata/{UserId}")]
        public async Task<JsonResult> GetEnrollmentData(string UserId)
        {
            var result = await repository.GetEnrollData(UserId);
            return Json(result);
        }
        [HttpPost("mid")]
        public async Task<JsonResult> MidtransAsync([FromBody] object course)
        {
            dynamic enr = JsonConvert.DeserializeObject<dynamic>(course.ToString());
            Enrollment enrollment = new Enrollment
            {
                UserId = enr.enrollment.userId,
                CourseId = enr.enrollment.courseId,
                StartDate = enr.enrollment.startDate,
                Status = (PayStatus)0
            };
            var enroll = repository.PostReturn(enrollment);

            enr.transaction_details.order_id = enroll.Id;
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(enr), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "U0ItTWlkLXNlcnZlci1wVEtmMlRfeXB4NnRIOVNyV3l4cDRaWFI6");
            var response = await httpClient.PostAsync("https://app.sandbox.midtrans.com/snap/v1/transactions", content);
            string apiResponse = await response.Content.ReadAsStringAsync();
            object obj = JsonConvert.DeserializeObject<object>(apiResponse);
            return Json(obj);
        }

        [HttpPost("statuschange")]
        public async Task<ActionResult> StatusChangeAsync([FromBody] object enrol)
        {
            dynamic enr = JsonConvert.DeserializeObject<dynamic>(enrol.ToString());
            string enrollmentId = enr.order_id;
            var result = await repository.Get(int.Parse(enrollmentId));
            if (enr.transaction_status == "pending")
            {
                result.Status = (PayStatus)0;
            }
            else if (enr.transaction_status == "cancel")
            {
                result.Status = (PayStatus)1;
            }
            else if (enr.transaction_status == "settlement")
            {
                result.Status = (PayStatus)2;
            }
            repository.Put(result);
            return Ok(result);
        }
        [HttpGet("CheckEnrollment/{userId}/{id}")]
        public async Task<JsonResult> CheckEnrollment(string userId, int id)
        {
            var result = await repository.CheckEnrollment(userId, id);
            return Json(result);
        }
    }
}
