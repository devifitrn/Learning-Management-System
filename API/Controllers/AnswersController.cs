using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : BaseController<Answer, AnswerRepository, int>
    {
        private readonly AnswerRepository answerRepository;
        public AnswersController(AnswerRepository answerRepository) : base(answerRepository)
        {
            this.answerRepository = answerRepository;
        }
    }
}
