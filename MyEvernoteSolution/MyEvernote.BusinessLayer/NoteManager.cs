using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;

namespace MyEvernote.BusinessLayer
{
    public class NoteManager
    {
        private Repository<Note> repo_note = new Repository<Note>();

        //notları listeleyen kod
        public List<Note> GetAllNote()
        {
            return repo_note.List();
        }

        //queryable versiyonu
        public IQueryable<Note> GetAllNoteQueryable()
        {
            return repo_note.ListQueryable();
        }
    }
}
