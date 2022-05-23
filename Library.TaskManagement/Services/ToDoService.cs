using Library.TaskManagement.Models;
using Newtonsoft.Json;
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

        public int NextId
        {
            get
            {
                if(!ToDos.Any())
                {
                    return 1;
                }

                return ToDos.Select(t => t.Id).Max() + 1;
            }
        }

        private static ToDoService current;

        public static ToDoService Current
        {
            get
            {
                if(current == null)
                {
                    current = new ToDoService();
                }

                return current;
            }
        }

        private ToDoService()
        {
            toDoList = new List<ToDo>();
        }

        public void Create(ToDo todo)
        {
            todo.Id = NextId;
            ToDos.Add(todo);
        }

        public void Update(ToDo? todo)
        {

        }

        public void Delete(int id)
        {
            var todoToDelete = toDoList.FirstOrDefault(t => t.Id == id);
            if(todoToDelete == null)
            {
                return;
            }
            toDoList.Remove(todoToDelete);
        }

        public void Load(string fileName)
        {
            var todosJson = File.ReadAllText(fileName);
            toDoList = JsonConvert.DeserializeObject<List<ToDo>>(todosJson) ?? new List<ToDo>();

        }

        public void Save(string fileName)
        {
            var todosJson = JsonConvert.SerializeObject(toDoList);
            File.WriteAllText(fileName, todosJson);
        }
    }
}
