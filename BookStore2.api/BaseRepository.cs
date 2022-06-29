using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore2.Models.ViewModels;

namespace BookStore2.Repository
{
    public class BaseRepository 
    {
        protected readonly BookStoreContext context = new BookStoreContext();
    }
}
