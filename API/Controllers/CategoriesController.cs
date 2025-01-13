using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, CategoryRepository, int>
    {
        private readonly CategoryRepository CategoryRepository;
        public CategoriesController(CategoryRepository CategoryRepository) : base(CategoryRepository)
        {
            this.CategoryRepository = CategoryRepository;
        }
    }
}
