using Library.TaskManagement.Models;
using Microsoft.AspNetCore.Mvc;
using TaskApplication.API.EC;

namespace TaskApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController: ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(ILogger<ToDoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Item> Get()
        {
            return new ToDoEC().Get();
        }
    }
}
