namespace Library.TaskManagement.Models
{
    public partial class ToDo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int AssignedUser { get; set; }
        public bool Completed { get; set; }
        public int Id { get; set; }
        public ToDo()
        {

        }

        public override string ToString()
        {
            return $"{Id} {Deadline:d} - {Name} :: {Description}";
        }
    }
}