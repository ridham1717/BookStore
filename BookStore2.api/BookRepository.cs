using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore2.Models.Models;
using BookStore2.Models.ViewModels;


namespace BookStore2.Repository
{
   
    public class BookRepository : BaseRepository
    {

        public ListResponse<Book> getBooks(int pageIndex , int pageSize , string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = context.Books.Where(c => keyword == null || c.Name.ToLower().Trim().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();   
            List<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Book>
            {
                Results = books,
                TotalRecords = totalRecords,
            };

        }

        public Book GetBook(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return context.Books.FirstOrDefault(c => c.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Book AddBook(Book book)
        {
            var entry = context.Books.Add(book);
            context.SaveChanges();
            return entry.Entity;

        }

        public Book UpdateBook(Book book)
        {
            var entry = context.Books.Update(book);
            context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            var book = context.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
                return false;

            context.Books.Remove(book);
            context.SaveChanges();
            return true;
        }

    }
}
