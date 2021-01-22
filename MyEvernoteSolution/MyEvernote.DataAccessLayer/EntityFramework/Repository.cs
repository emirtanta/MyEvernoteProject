using MyEvernote.Common;
using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.Abstract;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    //repository<T> where:class yazılmasının sebebi farklı tiplerin class'lar üzerinde işlem yapmasının engellenmesidir. Böylece sadece class üzerinde işlem yapılır. Tüm class'lar bu repository'yi kullanabilir
    public class Repository<T>:RepositoryBase,IRepository<T> where T:class
    {
        //private DatabaseContext db;

        private DbSet<T> _objectSet;

        

        //Set metodunun tekrar tekrar kullanılmaması için ve performans kaybının olmaması için Set metodu _objectSet değerine eşitlenerek işlemelerin daha performanslı olması sağlandı.
        public Repository()
        {
            //db = RepositoryBase.CreateContext();
            _objectSet = context.Set<T>();
        }

        //listeleme metodu
        public List<T> List()
        {
            return _objectSet.ToList();
        }

        //order by için
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        //belli kritere göre listeleme
        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        //ekleme metodu
        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); //TODO: işlem yapan kullanıcı adı yazılmalı
            }

            return Save();
        }

        //güncelleme metodu
        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); //TODO: işlem yapan kullanıcı adı yazılmalı
            }

            return Save();
        }

        //silme metodu
        public int Delete(T obj)
        {
            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;

            //    o.ModifiedOn = DateTime.Now;
            //    o.ModifiedUsername = "system"; //TODO: işlem yapan kullanıcı adı yazılmalı
            //}

            _objectSet.Remove(obj);

            return Save();
        }

        //tek bir veriyi getiren metot
        public T Find(Expression<Func<T,bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
