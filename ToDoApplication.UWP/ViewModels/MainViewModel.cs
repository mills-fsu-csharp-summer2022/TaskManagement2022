﻿using Library.TaskManagement.Models;
using Library.TaskManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ToDoApplication.UWP.Dialogs;
using Windows.UI.Xaml.Controls;

namespace ToDoApplication.UWP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public string Query { get; set; }
        public ItemViewModel SelectedItem { get; set; }
        private ItemService _itemService;

        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                if (_itemService == null)
                {
                    return new ObservableCollection<ItemViewModel>();
                }

                if(string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ItemViewModel>(_itemService.Items.Select(i => new ItemViewModel(i)));
                } else
                {
                    return new ObservableCollection<ItemViewModel>(
                        _itemService.Items.Where(i => i.Name.ToUpper().Contains(Query.ToUpper())
                            || i.Description.ToUpper().Contains(Query.ToUpper()))
                        .Select(i => new ItemViewModel(i)));
                }
                
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

        public async Task Add(ItemType iType)
        {
            ContentDialog diag = null;
            if(iType == ItemType.Task)
            {
                diag = new ToDoDialog();
            } else if(iType == ItemType.Appointment)
            {
                diag = new AppointmentDialog();
            } else if (iType == ItemType.Item)
            {
                diag = new ItemDialog();
            }else
            {
                throw new NotImplementedException();
            }

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
            if(SelectedItem != null)
            {
                ContentDialog diag = new ItemDialog(SelectedItem);
                //if(SelectedItem.IsToDo)
                //{
                //    diag = new ToDoDialog(SelectedItem.BoundToDo);
                //} else if(SelectedItem.IsAppointment)
                //{
                //    diag = new AppointmentDialog(SelectedItem.BoundAppointment);
                //}


                
                await diag.ShowAsync();
                NotifyPropertyChanged("Items");
            }

        }

        public void Save()
        {
            _itemService.Save();
        }

        public void Load()
        {
            _itemService.Load();
            NotifyPropertyChanged("Items");
        }

        public void Refresh()
        {
            NotifyPropertyChanged("Items");
        }
    }

    public enum ItemType{
        Task, Appointment, Item
    }
}
