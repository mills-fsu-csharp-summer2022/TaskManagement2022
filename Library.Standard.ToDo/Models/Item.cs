using Library.Standard.ToDo.Utility;
using Newtonsoft.Json;

namespace Library.TaskManagement.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class Item 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} :: {Description}";
        }

        //public Item(ItemViewModel ivm)
        //{

        //}
    }
}
