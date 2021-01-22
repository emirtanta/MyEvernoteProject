using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        //listeleme metodu
        List<T> List();

        //orderby ile ilgili 
        IQueryable<T> ListQueryable();

        //belli kritere göre listeleme
         List<T> List(Expression<Func<T, bool>> where);
        

        //ekleme metodu
        int Insert(T obj);
        

        //güncelleme metodu
        int Update(T obj);


        //silme metodu
        int Delete(T obj);


        //tek bir veriyi getiren metot
        T Find(Expression<Func<T, bool>> where);
        

         int Save();
        
    }
}
