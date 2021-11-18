using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Beauty.ViewModel
{
    class ApplicationViewModel : VM
    {
        public ApplicationViewModel()
        {
            DataBase = new DBViewModel();

            EditCommand = new RelayCommand<ServiceViewModel>(Edit);
            DeleteCommand = new RelayCommand<ServiceViewModel>(Delete);
            SaveCommand = new RelayCommand<object>(_ => Save());
            ChangeImageCommand = new RelayCommand<object>(_ => ChangeImage());
            AddServiceCommand = new RelayCommand<object>(_ => AddService());
            SortCostCommand = new RelayCommand<object>(_ => SortCost());
            SearchServiceCommand = new RelayCommand<object>(_ => SearchService());

            isVisibleArrowUp = "Visible";
            isVisibleArrowDown = "Hidden";
            filtervalue = "Все";
        }

        public DBViewModel DataBase { get; }

        // Команда редактирования объекта
        private void Edit(ServiceViewModel service) => SelectedService = service;

        private void ChangeImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выберите изображение";
            op.Filter = "Все форматы|*.jpg;*.jpeg;*.png|" +
              "JPEG|*.jpg;*.jpeg|" +
              "Png|*.png";

            if (op.ShowDialog() == true)
            {
                SelectedService.MainImage = op.FileName;
            }
        }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ChangeImageCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand LoginUser { get; }
        public ICommand AddServiceCommand { get; }
        public ICommand SortCostCommand { get; }
        public ICommand FilterValueCommand { get; }
        public ICommand SearchServiceCommand { get; }

        // Added service
        private void AddService() => DataBase.AddService(SelectedService);

        // Sort cost method
        private void SortCost()
        {
            if(isVisibleArrowUp == "Visible")
            {
                isVisibleArrowUp = "Hidden";
                isVisibleArrowDown = "Visibile";
            }
            else
            {
                isVisibleArrowUp = "Visible";
                isVisibleArrowDown = "Hidden";
            }
            DataBase.SortCost(isvisiblearrowup);
        }

        // Search service
        private void SearchService() => DataBase.SearchService(searchvalue, filtervalue);

        // Delete item
        private void Delete(ServiceViewModel service) => DataBase.DeleteServiceModel(service);

        // Save item
        private void Save() => DataBase.UpdateServiceModel(SelectedService);

        // PROPERTIES
        public int RoleUser { get; set; }
        public string isVisible { get; set; }

        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string filtervalue;
        public string FilterValue
        {
            get => filtervalue;
            set
            {
                filtervalue = value;
                OnPropertyChanged("FilterValue");
            }
        }

        private string searchvalue;
        public string SearchValue
        {
            get => searchvalue;
            set
            {
                searchvalue = value;
                OnPropertyChanged("SearchValue");
            }
        }

        private string isvisiblearrowdown;
        public string isVisibleArrowDown
        {
            get => isvisiblearrowdown;
            set
            {
                isvisiblearrowdown = value;
                OnPropertyChanged("isVisibleArrowDown");
            }
        }

        private string isvisiblearrowup;
        public string isVisibleArrowUp
        {
            get => isvisiblearrowup;
            set
            {
                isvisiblearrowup = value;
                OnPropertyChanged("isVisibleArrowUp");
            }
        }

        private ServiceViewModel selectedService;
        public ServiceViewModel SelectedService
        {
            get => selectedService;
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedService");
            }
        }
    }
}
