using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models
{
    [Table("provider")]
    public class Provider : BaseModel
    {
        private long id;
        private string name;
        private string address;
        private string phone;
        private string contactPerson;
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

        [Column("address")]
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }

        [Column("phone")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        [Column("contact_person")]
        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; OnPropertyChanged(nameof(ContactPerson)); }
        }

        public ICollection<Delivery> Deliveries
        {
            get { return deliveries; }
            set { deliveries = value; }
        }
    }
}
