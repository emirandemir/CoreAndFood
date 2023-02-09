using CoreAndFood.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAndFood.Repositories
{
    public class GenericRepository <T> where T : class,new()
    {
        Context context = new Context();

        public List<T> TList()     //Categori Listeleme İşlemi
        {
            return context.Set<T>().ToList();
        }

        public void TAdd(T p)  //Categori Ekleme
        {
            context.Set<T>().Add(p);
            context.SaveChanges();
        }

        public void TDelete(T p)  //Kategori Silme
        {
            context.Set<T>().Remove(p);
            context.SaveChanges();
        }
        public void TUpdate(T p)     //Kategori Güncelleme
        {
            context.Set<T>().Update(p);
            context.SaveChanges();
        }

        public T TFind(int id)   //Kategori Arama
        {
          return context.Set<T>().Find(id);
            
        }

        public List<T> TList(string p)   //include işlemi
        {
            return context.Set<T>().Include(p).ToList();
        }
    }
}

