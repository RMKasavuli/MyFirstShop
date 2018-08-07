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
        public DataContext ()
            : base("DefaultConnection")//to look in webconfig for the DefaultConnection string 
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategoriess { get; set; }
    }
}
