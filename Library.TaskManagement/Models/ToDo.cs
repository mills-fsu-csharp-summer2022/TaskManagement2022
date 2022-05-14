namespace Library.TaskManagement.Models
{
    public partial class ToDo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int AssignedUser { get; set; }
        public bool Completed { get; set; }

        public ToDo()
        {

        }

        public override string ToString()
        {
            return $"{Deadline:d} - {Name} :: {Description}";
        }
    }
}