using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        //repository ile sorgu kategori listesini getirdik
        private Repository<Category> repo_category = new Repository<Category>();

        //commnet
        private Repository<Comment> repo_comment = new Repository<Comment>();

        //note
        private Repository<Note> repo_note = new Repository<Note>();

        public Test()
        {
            //DataAccessLayer.DatabaseContext db = new DataAccessLayer.DatabaseContext();

            //database yoksa oluşturma işlemini yapar
            //db.Database.CreateIfNotExists();
            //db.EvernoteUsers.ToList();


            //db.EvernoteUsers.ToList();

            
            
            List<Category> categories= repo_category.List();
        }

        //insert için
        public void InsertTest()
        {
            

            //örnek bir user eklendi test amaçlı
            int result = repo_user.Insert(new EvernoteUser()
            {
                Name="aaa",
                Surname="bbb",
                Email="emirtanta@hotmail.com",
                ActivateGuid=Guid.NewGuid(),
                IsActive=true,
                IsAdmin=true,
                Username="aabb",
                Password="111",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now.AddMinutes(5),
                ModifiedUsername="aabb"
            });
        }

        //update testi
        public void UpdateTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "aabb");

            //eğer kullanıcıyı bulursan
            if (user!=null)
            {
                user.Username = "xxx";

               int result= repo_user.Update(user);
            }
        }

        //delete testi
        public void DeleteTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "xxx");

            if (user!=null)
            {
               int result= repo_user.Delete(user);
            }
        }

        //comment testi bölümü
        public void CommentTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Id == 1);
            Note note = repo_note.Find(x => x.Id == 3);

            Comment comment = new Comment()
            {
                Text = "bu bir testir",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "muratbasaren",
                Note = note,
                Owner = user
            };

            repo_comment.Insert(comment);
        }
    }
}
