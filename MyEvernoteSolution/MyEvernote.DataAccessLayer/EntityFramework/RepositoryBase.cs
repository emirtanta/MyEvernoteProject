using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        protected static DatabaseContext context;

        private static object _lockSync=new object();

        //RepositoryBase sınıfının new'lenmemesi için tanımlandı
        protected RepositoryBase()
        {
            CreateContext();
        }

        //veritabanının 1 defa oluşturulması sağlanır
        private static void CreateContext()
        {
            if (context==null)
            {
                //lock tanımlanmasının sebebi çoklu tread yapılarında oluşacak tread çakışmasını önlemektir
                lock (_lockSync)
                {
                    if (context==null)
                    {
                        context = new DatabaseContext();
                    }
                    
                }
                
            }
            
        }
    }
}
