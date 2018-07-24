using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T :BaseEntity//generic class
    {
        //objects
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;
        //constructor
        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }
        }
        //memory
        public void Commit()
        {
            cache[className] = items;
        }


        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T ttoUpdate = items.Find(i => i.Id == t.Id);
            if (ttoUpdate!=null)
            {
                ttoUpdate = t;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null )
            {
                return t;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T ttoDelete = items.Find(i => i.Id == Id);
            if (ttoDelete != null)
            {

                items.Remove(ttoDelete);
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }
    }
}
