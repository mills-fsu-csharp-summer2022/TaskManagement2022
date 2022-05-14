using Library.TaskManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManagement.Services
{

    public class ToDoService
    {
        private List<ToDo> toDoList;
        public List<ToDo> ToDos
        {
            get
            {
                return toDoList;
            }
        }

        public ToDoService()
        {
            toDoList = new List<ToDo>();
        }

        public void Create(ToDo todo)
        {
            ToDos.Add(todo);
        }

        public void Update(ToDo todo)
        {

        }

        public void Delete(int index)
        {
            toDoList.RemoveAt(index);
        }
    }
}
