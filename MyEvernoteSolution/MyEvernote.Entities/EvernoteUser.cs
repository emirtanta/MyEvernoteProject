using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("EvernoteUsers")] //tablo adı
    public class EvernoteUser:MyEntityBase
    {
        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(25)]
        public string Surname { get; set; }

        [Required,StringLength(25)]
        public string Username { get; set; }

        [Required,StringLength(70)]
        public string Email { get; set; }

        [Required,StringLength(25)]
        public string Password { get; set; }

        //kullanıcıyı aktif etmek için tanımlandı
        public bool IsActive { get; set; }

        //aktiflik kodu için tanımlandı
        [Required]
        public Guid ActivateGuid { get; set; }

        //admin olma kontrolü için
        [Required]
        public bool IsAdmin { get; set; }

        //kategori-not ilişkisi(bire çok ilişki)
        public virtual List<Note> Notes { get; set; }

        //kategori-yorum ilişkisi(bire çok ilişki)
        public virtual List<Comment> Comments { get; set; }

        //bir kullanıcının birdden çok like vardır
        public virtual List<Liked> Likes { get; set; }
    }
}
