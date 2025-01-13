using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : BaseController<Quiz, QuizRepository, int>
    {
        private readonly QuizRepository quizRepository;
        public QuizzesController(QuizRepository quizRepository) : base(quizRepository)
        {
            this.quizRepository = quizRepository;
        }
        [HttpPost("AddQuiz")]
        public virtual ActionResult AddQuiz(QuizPostVM quiz)
        {
            var result = quizRepository.InsertQuiz(quiz);
            return Ok(new { status = 200, result, message = "Data Berhasil Ditambahkan" });
        }
    }
}
