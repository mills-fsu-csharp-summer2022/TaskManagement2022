namespace Library.TaskManagement.Models
{
    public class Item 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} :: {Description}";
        }


    }
}
