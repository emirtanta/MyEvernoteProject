using MyEvernote.Entities;
using MyEvernote.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    //TempData ilekategorilerin listelenmesi sağlar
    public class CategoryManager
    {
        private Repository<Category> repo_category = new Repository<Category>();

        //kategorileri listeleyen metot
        public List<Category> GetCategories()
        {
            return repo_category.List();
        }

        //seçilen kategoriye göre notu gösteren kod
        public Category GetCategoryById(int id)
        {
            return repo_category.Find(x => x.Id == id);
        }
    }
}
