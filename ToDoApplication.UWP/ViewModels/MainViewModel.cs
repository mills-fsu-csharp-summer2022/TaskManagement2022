using Library.TaskManagement.Models;
using Library.TaskManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ToDoApplication.UWP.Dialogs;

namespace ToDoApplication.UWP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public string Query { get; set; }
        public Item SelectedItem { get; set; }
        private ItemService _itemService;

        public ObservableCollection<Item> Items
        {
            get
            {
                if (_itemService == null)
                {
                    return new ObservableCollection<Item>();
                }
                return new ObservableCollection<Item>(_itemService.Items);
            }
        }

        public MainViewModel()
        {
            _itemService = ItemService.Current;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Add()
        {
            var diag = new ToDoDialog();
            await diag.ShowAsync();
            NotifyPropertyChanged("Items");
        }

        public void Remove()
        {
            var id = SelectedItem?.Id ?? -1;
            if(id >= 1)
            {
                _itemService.Delete(SelectedItem.Id);
            }
            NotifyPropertyChanged("Items");
        }

        public async void Update()
        {
            //var id = SelectedItem?.Id ?? -1;
            //if (id >= 1)
            //{
            //    _itemService.Update(SelectedItem);
            //}
            //NotifyPropertyChanged("Items");
            if(SelectedItem != null)
            {
                var diag = new ToDoDialog(SelectedItem);
                await diag.ShowAsync();
                NotifyPropertyChanged("Items");
            }

        }
    }
}
