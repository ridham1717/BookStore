﻿using Microsoft.AspNetCore.Mvc;
using BookStore2.Repository;
using BookStore2.Models.Models;
using BookStore2.Models.ViewModels;
using System.Net;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("api/Book")]
    public class BookController : Controller
    {
        BookRepository _bookRepository = new BookRepository();
        [Route("GetBooks")]
        [HttpGet]
        public IActionResult getBooks(string keyword,int pageIndex=1, int pageSize=10)

        {
            var books = _bookRepository.getBooks(pageIndex, pageSize, keyword);

            ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel()),
                TotalRecords = books.TotalRecords
            };
            return Ok(listResponse);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
           var book = _bookRepository.GetBook(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(new BookModel(book));
        }

        [Route("addBook")]
        [HttpPost]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]

        public IActionResult AddBook(BookModel model)
        {
            if(model == null)
            {
                return BadRequest();    
            }
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,

            };

            var response = _bookRepository.AddBook(book);
            BookModel bookModel = new BookModel(response);



            return Ok(bookModel);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = _bookRepository.UpdateBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
                return BadRequest("id is null");

            var response = _bookRepository.DeleteBook(id);
            return Ok(response);
        }
    }


}

    

   
