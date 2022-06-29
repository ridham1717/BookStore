using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore2.Models.ViewModels;
using BookStore2.Models.Models;

namespace BookStore2.Repository
{
    public class CategotyRepository : BaseRepository
    {
        public ListResponse<Category> GetCategories (int pageIndex , int pageSize , string keyword)

        {
            keyword = keyword?.ToLower()?.Trim();
            var query = context.Categories.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Category> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Category>()
            {
                Results = categories,
                TotalRecords = totalReocrds,
            };


        }

        public Category  GetCategory(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return context.Categories.FirstOrDefault(c => c.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Category AddCategory(Category category)
        {
          var entry =   context.Categories.Add(category);
            context.SaveChanges();
           return entry.Entity;
        }

        public Category UpdateCategory(Category category)
        {
            var entry = context.Categories.Update(category);
            context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;

            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }




    }
}
