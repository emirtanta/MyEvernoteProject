using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{

    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        //veritabanındaki tabloların içerisine örnek data eklemek için tanımlandı(isteğe bağlı)
        protected override void Seed(DatabaseContext context)
        {
            //adding admin user
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Murat",
                Surname = "Başaren",
                Email = "emirtanta@hotmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "muratbasaren",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "muratbasaren"
            };

            //yeni bir kullanıcı (satandart kullanıcı)
            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "Murat",
                Surname = "Başaren",
                Email = "emirtanta@hotmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "kadirbasaren",
                Password = "654321",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "muratbasaren"
            };

            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);

            //rasstgele kullanıcı oluşturma işlemi yapıldı
            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Name = FakeData.NameData.GetFemaleFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };

                context.EvernoteUsers.Add(user);
            }

            context.SaveChanges();

            //userList for using
            List<EvernoteUser> userList = context.EvernoteUsers.ToList();

            //örnek 10 adet kategori oluşturma
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "muratbaseren"
                };

                context.Categories.Add(cat);

                //rastgele sayıda not ekledik(fakedata ile )
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); i++)
                {
                    EvernoteUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];

                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),//rastgele 5 ile 25 kelime arasında başlık oluşturdu
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        //Category=cat,
                        IsDraft=false,
                        LikeCount=FakeData.NumberData.GetNumber(1,9),
                        Owner=owner, //owner otomatik olarak tanımlandı
                        CreatedOn=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        ModifiedOn=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        ModifiedUsername=owner.Username,
                    };

                    cat.Notes.Add(note);

                    //adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        EvernoteUser comment_owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];

                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            //Note=note,
                            Owner= comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username,
                        };

                        note.Comments.Add(comment);
                    }

                    //rastgele beğeniler eklendi (1ile 5 arasında kullanıcı beğendi rastgele fake data)
                    
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m],
                        };

                        note.Likes.Add(liked);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
