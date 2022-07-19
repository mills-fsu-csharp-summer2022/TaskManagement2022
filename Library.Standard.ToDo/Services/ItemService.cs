using Library.Standard.ToDo.Utility;
using Library.TaskManagement.Models;
using Library.TaskManagement.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManagement.Services
{

    public class ItemService
    {
        private string persistPath 
            = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}";
        private ListNavigator<Item> listNavigator;
        private List<Item> itemList;
        public List<Item> Items
        {
            get
            {
                var itemsJson = new WebRequestHandler().Get("http://localhost:5017/WeatherForecast");
                return itemList;
            }
        }

        public int NextId
        {
            get
            {
                if (!Items.Any())
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
                if (current == null)
                {
                    current = new ItemService();
                }

                return current;
            }
        }

        private ItemService()
        {
            var todosJson = new WebRequestHandler().Get("http://localhost:5017/Item").Result;
            itemList = JsonConvert.DeserializeObject<List<Item>>(todosJson);

            listNavigator = new ListNavigator<Item>(itemList);

        }

        public void AddOrUpdate(Item todo)
        {
            if(todo is ToDo)
            {
                var response = new WebRequestHandler().Post("http://localhost:5017/ToDo/AddOrUpdate", todo).Result;
                var newToDo = JsonConvert.DeserializeObject<ToDo>(response);

                var oldVersion = itemList.FirstOrDefault(i => i.Id == newToDo.Id);
                if(oldVersion != null)
                {
                    var index = itemList.IndexOf(oldVersion);
                    itemList.RemoveAt(index);
                    itemList.Insert(index, newToDo);
                } else
                {
                    itemList.Add(newToDo);
                }

            } else if (todo is Appointment)
            {
                var response = new WebRequestHandler().Post("http://localhost:5017/Appointment", todo).Result;
                var newToDo = JsonConvert.DeserializeObject<Appointment>(response);
                itemList.Add(newToDo);
            }
            
        }

        public void Delete(int id)
        {
            var response = new WebRequestHandler().Get($"http://localhost:5017/ToDo/Delete/{id}");
            var todoToDelete = itemList.FirstOrDefault(t => t.Id == id);
            if (todoToDelete == null)
            {
                return;
            }
            itemList.Remove(todoToDelete);

        }

        public void Load(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = $"{persistPath}\\SaveData.json";
            }
            else
            {
                fileName = $"{persistPath}\\{fileName}.json";
            }

            var todosJson = File.ReadAllText(fileName);
            itemList = JsonConvert.DeserializeObject<List<Item>>
                (todosJson, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })
                ?? new List<Item>();

        }

        public void Save(string fileName = null)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                fileName = $"{persistPath}\\SaveData.json";
            } else
            {
                fileName = $"{persistPath}\\{fileName}.json";
            }
            var todosJson = JsonConvert.SerializeObject(itemList
                , new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText(fileName, todosJson);
        }

        //GROSS
        //public IEnumerable<Item> Search(string query)
        //{
        //    return Items.Where(i => i.Description.Contains(query) || i.Name.Contains(query));
        //}

        //Stateful Implementation
        private string _query;
        private bool _sort;

        public IEnumerable<Item> Search(string query)
        {
            _query = query;
            return ProcessedList;
        }

        public IEnumerable<Item> ProcessedList{
            get
            {
                if(string.IsNullOrEmpty(_query))
                {
                    return Items;
                }
                return Items
                    .Where(i => string.IsNullOrEmpty(_query) ||( i.Description.Contains(_query)
                        || i.Name.Contains(_query))) //search
                    .OrderBy(i => i.Name);          //sort
            }
        }

    }
}
