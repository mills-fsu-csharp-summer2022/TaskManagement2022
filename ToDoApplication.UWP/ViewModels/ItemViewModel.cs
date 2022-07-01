using Library.TaskManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ToDoApplication.UWP.ViewModels
{
    public class ItemViewModel:INotifyPropertyChanged
    {
        public string Name { 
            get
            {
                return BoundItem?.Name ?? string.Empty;
            }

            set
            {
                if (BoundItem == null)
                {
                    return;
                }

                BoundItem.Name = value;
            }
        }
        public string Description {
            get
            {
                return BoundItem?.Description ?? string.Empty;
            }

            set
            {
                if (BoundItem == null)
                {
                    return;
                }

                BoundItem.Description = value;
            }
        }
        public int Id {
            get
            {
                return BoundItem?.Id ?? 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return $"{Id} {Name} :: {Description}";
        }

        public string Title
        {
            get
            {
                return "TEST";
            }
        }

        public Item BoundItem { 
            get
            {
                if(BoundToDo != null)
                {
                    return BoundToDo;
                }

                return BoundAppointment;
            } 
        }

        public Visibility IsCompletable
        {
            get
            {
                return BoundAppointment == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsAppointment
        {
            get
            {
                return BoundAppointment != null;
            }
        }

        public bool IsToDo
        {
            get
            {
                return BoundToDo != null;
            }
        }

        private ToDo boundToDo;
        public ToDo BoundToDo
        {
            get
            {
                return boundToDo;
            }
        }

        private Appointment boundAppointment;
        public Appointment BoundAppointment
        {
            get
            {
                return boundAppointment;
            }
        }

        public ItemViewModel()
        {
            boundAppointment = null;
            boundToDo = new ToDo();
        }

        public ItemViewModel(Item i)
        {
            if(i == null)
            {
                return;
            }

            if(i is Appointment)
            {
                boundAppointment = i as Appointment;
            } else if(i is ToDo)
            {
                boundToDo = i as ToDo;
            }
        }
    }
}
