using Library.TaskManagement.Models;
using Library.TaskManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using ToDoApplication.UWP.Dialogs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToDoApplication.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public string Query { get; set; }
        private ItemService _itemService;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Item SelectedItem { get; set; }

        public ObservableCollection<Item> Items {
            get
            {
                if(_itemService == null)
                {
                    return new ObservableCollection<Item>();
                }
                return new ObservableCollection<Item>(_itemService.Items);
            }
        }
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;

            _itemService = ItemService.Current;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var diag = new ToDoDialog();
            await diag.ShowAsync();
            NotifyPropertyChanged("Items");

            //var page = new SecondaryPage();
            Frame.Navigate(typeof(SecondaryPage));
            
        }

        public void Refresh()
        {

        }
    }
}
