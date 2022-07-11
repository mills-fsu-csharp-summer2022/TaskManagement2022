using Library.TaskManagement.Models;
using TaskApplication.API.Database;

namespace TaskApplication.API.EC
{
    public class ToDoEC
    {
        public List<Item> Get()
        {
            return FakeDatabase.ToDos;
        }
    }
}
