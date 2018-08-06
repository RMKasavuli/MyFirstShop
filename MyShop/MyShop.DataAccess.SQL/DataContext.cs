using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext: DbContext
    {
        public DataContext()
            :base("DefaultConnection") {//read from webconfig or appconfig 
        }

        //create database sets
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Productcategories { get; set; }
    }
}
