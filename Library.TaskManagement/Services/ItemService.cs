using Library.TaskManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManagement.Services
{

    public class ItemService
    {
        private List<Item> itemList;
        public List<Item> Items
        {
            get
            {
                return itemList;
            }
        }

        public int NextId
        {
            get
            {
                if(!Items.Any())
                {
                    return 1;
                }

                return Items.Select(t => t.Id).Max() + 1;
            }
        }

        private static ItemService current;

        public static ItemService Current
        {
            get
            {
                if(current == null)
                {
                    current = new ItemService();
                }

                return current;
            }
        }

        private ItemService()
        {
            itemList = new List<Item>();
        }

        public void Create(Item todo)
        {
            todo.Id = NextId;
            Items.Add(todo);
        }

        public void Update(Item? todo)
        {

        }

        public void Delete(int id)
        {
            var todoToDelete = itemList.FirstOrDefault(t => t.Id == id);
            if(todoToDelete == null)
            {
                return;
            }
            itemList.Remove(todoToDelete);
        }

        public void Load(string fileName)
        {
            var todosJson = File.ReadAllText(fileName);
            itemList = JsonConvert.DeserializeObject<List<Item>>(todosJson) ?? new List<Item>();

        }

        public void Save(string fileName)
        {
            var todosJson = JsonConvert.SerializeObject(itemList);
            File.WriteAllText(fileName, todosJson);
        }
    }
}
