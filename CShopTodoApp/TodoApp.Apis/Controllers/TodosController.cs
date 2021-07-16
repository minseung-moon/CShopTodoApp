using CShopTodoApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Apis.Controllers
{
    [Route("api/[Controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _repository;

        public TodosController()
        {
            _repository = new TodoRepositoryJson(@"C:\Temp\Todos.json"); 
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost]
        public IActionResult Add([FromBody]Todo dto)
        {
            _repository.Add(dto);
            return Ok(dto);
        }
    }
}