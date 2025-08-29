using CodeCommunityFlow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.Repository
{

    public interface ICategoryRepository
    {
        void InsertCategory(Categories categories);
        void UpdateCategory(Categories categories);
        void DeleteCategory(int CategoryId);
        Categories GetCategoryByID(int CategoryId);
        List<Categories> GetAllCategory();


    }
    public class CategoryRepository : ICategoryRepository
    {
        CodeCommunityFlowDbContext db;
        public CategoryRepository()
        {
            db = new CodeCommunityFlowDbContext();
        }

        public List<Categories> GetAllCategory()
        {
            List<Categories> categories = db.Categories.ToList();
            return categories;
        }

        public Categories GetCategoryByID(int CategoryId)
        {
            Categories categoriesResult = db.Categories.Where(c => c.CategoryID == CategoryId).FirstOrDefault();
            return categoriesResult;
        }
        public void InsertCategory(Categories categories)
        {
            db.Categories.Add(categories);
            db.SaveChanges();
        }
        public void UpdateCategory(Categories categoryUpdate)
        {
            Categories category = db.Categories.Where(c => c.CategoryID == categoryUpdate.CategoryID).FirstOrDefault();
            if (category != null)
            {
                category.CatagoryName = categoryUpdate.CatagoryName;
                db.SaveChanges();
            }
        }

        public void DeleteCategory(int CategoryId)
        {
            Categories categoryDel = db.Categories.Where(c => c.CategoryID == CategoryId).FirstOrDefault();
            if (categoryDel != null)
            {
                db.Categories.Remove(categoryDel);
                db.SaveChanges();
            }

        }

    }
}
