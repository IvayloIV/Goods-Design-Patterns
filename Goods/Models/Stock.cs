using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models
{
    [Table("stock")]
    public class Stock : BaseModel
    {
        private long id;
        private string name;
        private DateTime creationDate;
        private int daysValidTo;
        private double price;
        private string measure;
        private ICollection<Delivery> deliveries;

        [Key, Column("id")]
        public long Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        [Column("name")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [Column("creation_date")]
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; OnPropertyChanged(nameof(CreationDate)); }
        }

        [Column("days_valid_to")]
        public int DaysValidTo
        {
            get { return daysValidTo; }
            set { daysValidTo = value; OnPropertyChanged(nameof(DaysValidTo)); }
        }

        [Column("price")]
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        [Column("measure")]
        public string Measure
        {
            get { return measure; }
            set { measure = value; OnPropertyChanged(nameof(Measure)); }
        }

        public ICollection<Delivery> Deliveries
        {
            get { return deliveries; }
            set { deliveries = value; }
        }
    }
}
