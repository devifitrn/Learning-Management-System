
﻿using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("/[controller]")]
    [Controller]
    public class CategoriesController : BaseController<Category, CategoryRepository, int>
    {
        private readonly CategoryRepository repository;
        public CategoriesController(CategoryRepository repository) : base(repository)
        {
            this.repository = repository;
        }

    }
}
