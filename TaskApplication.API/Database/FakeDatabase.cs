using Library.TaskManagement.Models;

namespace TaskApplication.API.Database
{
    public static class FakeDatabase
    {
        public static List<Item> Items { get; set; }

        public static List<Item> ToDos = new List<Item> { 
            new ToDo { Name = "Test ToDo1", Description = "Description 1", Id =1},
            new ToDo { Name = "Test ToDo2", Description = "Description 2", Id = 2},
            new ToDo { Name = "Test ToDo3", Description = "Description 3", Id = 3},
            new ToDo { Name = "Test ToDo4", Description = "Description 4", Id = 4},
            new ToDo { Name = "Test ToDo5", Description = "Description 5", Id = 5}
        };

        public static List<Appointment> Appointments = new List<Appointment>();
    }
}
