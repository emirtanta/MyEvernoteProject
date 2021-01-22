using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]//primary key ve otomatik artan şeklinde tanımlandı
        public int Id { get; set; }

        //eklenme zamanı
        [Required]
        public DateTime CreatedOn { get; set; }

        //düzenleme zamanı
        [Required]
        public DateTime ModifiedOn { get; set; }

        //kim taradından düzenlendiği
        [Required]
        public string ModifiedUsername { get; set; }
    }
}
