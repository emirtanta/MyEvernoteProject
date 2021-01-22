using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class DatabaseContext:DbContext
    {
        //veritabanındaki tablolara karşılık gelen DbSetler tanımlandı
        public DbSet<EvernoteUser> EvernoteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        //veritabanı içinde veri olmadan oluşturulmuştu.İçerisine örnek veriler ekleme işlemi için silip tekrar yüklenmesi gerektiğinden dolayı aşağıdaki kod bloğu oluşturuldu.Bu bölüm isteğe bağlı olup kullanılmayabilir
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
