using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.ViewModel
{
    class ServiceViewModel : VM
    {
        private Service_import model;
        public ServiceViewModel(Service_import ServiceModel)
        {
            model = ServiceModel;

            Id = model.id.ToString();
            NameService = model.NameService;
            Cost = model.Cost.ToString();
            Duration = model.Duration;
            MainImage = model.MainImage;
            Discount = model.Discount;
            CostDiscount = model.CostDiscount;
        }

        public ServiceViewModel()
        {
        }

        public string Id { get; set; }

        private int? costdiscount;
        public int? CostDiscount
        {
            get => costdiscount;
            set => SetField(ref costdiscount, value);
        }

        private string nameservice;
        public string NameService
        {
            get => nameservice;
            set => SetField(ref nameservice, value);
        }

        private string cost;
        public string Cost
        {
            get => cost;
            set => SetField(ref cost, value);
        }

        private int discount;
        public int Discount
        {
            get => discount;
            set => SetField(ref discount, value);
        }

        private string duration;
        public string Duration
        {
            get => duration;
            set => SetField(ref duration, value);
        }

        private string mainimage;
        public string MainImage
        {
            get => mainimage;
            set => SetField(ref mainimage, value);
        }
    }
}
