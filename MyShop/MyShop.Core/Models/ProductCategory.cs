using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory:BaseEntity
    {
        //public string Id { get; set; }, base entity already has an Id
        public string Category { get; set; }

        //constructor to generate Id whenever a category is created, base entity already has an Id
        //public ProductCategory ()
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}
    }
}
