using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class AnswerRepository : GeneralRepository<MyContext, Answer, int>
    {
        private readonly MyContext myContext;
        public AnswerRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
