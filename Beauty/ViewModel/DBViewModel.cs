using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Beauty.ViewModel
{
    class DBViewModel
    {
        private BeautyEntities DataBaseModel;
        public DBViewModel()
        {
            DataBaseModel = new BeautyEntities();
            LoadData();
        }

        public ObservableCollection<ServiceViewModel> Services { get; private set; }

        public void UpdateServiceModel(ServiceViewModel SelectedService)
        {
            if(SelectedService != null)
            {
                int id = Convert.ToInt32(SelectedService.Id);
                var item = DataBaseModel.Service_import.FirstOrDefault(x => x.id == id);
                if (item != null)
                {
                    item.NameService = SelectedService.NameService;
                    item.Duration = SelectedService.Duration;
                    item.Cost = int.Parse(SelectedService.Cost);
                    item.MainImage = SelectedService.MainImage;
                    item.Discount = SelectedService.Discount;
                    DataBaseModel.SaveChanges();

                    MessageBox.Show("Данные успешно обновлены!");
                }
            }
        }

        // Сортировка
        public void SortCost(string isEnabledSortBy)
        {
            if(isEnabledSortBy == "Visible")
            {
                var item = DataBaseModel.Service_import.OrderBy(o => o.Cost);
                Services.Clear();
                foreach (var service in item)
                {
                    Services.Add(new ServiceViewModel(service));
                }
            }
            else
            {
                var item = DataBaseModel.Service_import.OrderByDescending(o => o.Cost);
                Services.Clear();
                foreach (var service in item)
                {
                    Services.Add(new ServiceViewModel(service));
                }
            }
        }

        // Поиск
        public void SearchService(string value, string filtervalue)
        {
            if(filtervalue != "Все")
            {
                // От 5% до 10%
                // Разделим процент скидки на "От" и "До"
                string start = "";
                string end = "";
                int k = 0;
                for (int i = 0; i < filtervalue.Length; i++)
                {
                    if (filtervalue[i] == '%')
                    {
                        k = 1;
                    }
                    else if (char.IsDigit(filtervalue[i]))
                    {
                        if (k == 0)
                        {
                            start += filtervalue[i];
                        }
                        else
                        {
                            end += filtervalue[i];
                        }
                    }
                }
                int one = Convert.ToInt32(start);
                int two = Convert.ToInt32(end);


                if (!String.IsNullOrEmpty(value))
                {
                    var item = DataBaseModel.Service_import.Where(w => w.NameService == value && w.Discount >= one && w.Discount < two);
                    Services.Clear();
                    foreach (var service in item)
                    {
                        Services.Add(new ServiceViewModel(service));
                    }
                }
                else
                {
                    var item = DataBaseModel.Service_import.Where(w => w.Discount >= one && w.Discount < two).OrderBy(o => o.Cost);
                    Services.Clear();
                    foreach (var service in item)
                    {
                        Services.Add(new ServiceViewModel(service));
                    }
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(value))
                {
                    var item = DataBaseModel.Service_import.Where(w => w.NameService == value);
                    Services.Clear();
                    foreach (var service in item)
                    {
                        Services.Add(new ServiceViewModel(service));
                    }
                }
                else
                {
                    var item = DataBaseModel.Service_import.OrderBy(o => o.Cost);
                    Services.Clear();
                    foreach (var service in item)
                    {
                        Services.Add(new ServiceViewModel(service));
                    }
                }
            }
        }

        // Добавление услуги
        public void AddService(ServiceViewModel SelectedService)
        {
            // Нельзя иметь одинаковое название услуги
            var item = DataBaseModel.Service_import.Where(w => w.NameService == SelectedService.NameService).FirstOrDefault();

            string getDurationNumber = "";
            for (int i = 0; i < SelectedService.Duration.Length; i++)
            {
                if(SelectedService.Duration[i] >= '0' && SelectedService.Duration[i] <= '9')
                {
                    getDurationNumber += SelectedService.Duration[i];
                }
            }

            if (item != null)
            {
                MessageBox.Show("Услуга с таким названием уже существует!");
            }
            else if (Convert.ToInt32(getDurationNumber) > 240)
            {
                MessageBox.Show("Услуга не должна длиться более 4-х часов!");
            }
            else
            {
                // Посчитаем с учётом скидки
                int resultDiscount = 0;
                double costDiscount = Convert.ToDouble(SelectedService.Cost) * 0.01 * (100 - Convert.ToDouble(SelectedService.Discount));
                resultDiscount = Convert.ToInt32(costDiscount);
                Service_import addService = new Service_import
                {
                    NameService = SelectedService.NameService,
                    MainImage = SelectedService.MainImage,
                    Duration = SelectedService.Duration,
                    Cost = int.Parse(SelectedService.Cost),
                    Discount = SelectedService.Discount,
                    CostDiscount = resultDiscount
                };

                DataBaseModel.Service_import.Add(addService);
                DataBaseModel.SaveChanges();

                Services.Add(new ServiceViewModel(addService));

                MessageBox.Show("Услуга успешно добавлена!");
            }
        }

        public void DeleteServiceModel(ServiceViewModel SelectedService)
        {
            int id = Convert.ToInt32(SelectedService.Id);
            var item = DataBaseModel.Service_import.FirstOrDefault(x => x.id == id);
            DataBaseModel.Service_import.Attach(item);
            DataBaseModel.Service_import.Remove(item);
            DataBaseModel.SaveChanges();
            Services.Remove(SelectedService);
        }

        // Заполняем изначальую коллекцию при загрузке
        private void LoadData()
        {
            Services = new ObservableCollection<ServiceViewModel>();
            foreach (var service in DataBaseModel.Service_import.OrderBy(o=>o.Cost))
            {
                service.MainImage.Replace("\\", "/");
                Services.Add(new ServiceViewModel(service));
            }
        }
    }
}
