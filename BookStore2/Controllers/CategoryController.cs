using Microsoft.AspNetCore.Mvc;
using BookStore2.Models.Models;
using BookStore2.Repository;
using BookStore2.Models.ViewModels;
using System.Net;

namespace BookStore2.Controllers
{
    [Route("api/category")]
    [ApiController]

    public class CategoryController : Controller
    {
        CategotyRepository _categotyRepository = new CategotyRepository();

        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCatogires(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var categories = _categotyRepository.GetCategories(pageIndex, pageSize, keyword);
            ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
            {
                Results = categories.Results.Select(c => new CategoryModel(c)),
                TotalRecords = categories.TotalRecords,
            };

            return Ok(listResponse);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetCategory(int id)
        {
            var categories = _categotyRepository.GetCategory(id);

            if (categories == null)
            {
                return NotFound();
            }

            CategoryModel categorymodel = new CategoryModel(categories);


            return Ok(categorymodel);
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddCategory(CategoryModel model)
        {
            Category category = new Category();
            {
                category.Id = model.Id;
                category.Name = model.Name;
            };
            
            var response = _categotyRepository.AddCategory(category);
            
             CategoryModel categoryModel = new CategoryModel(response);


            return Ok(categoryModel);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var response = _categotyRepository.UpdateCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);

            return Ok(categoryModel);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            if (id == 0)
                return BadRequest("id is null");

            var response = _categotyRepository.DeleteCategory(id);
            return Ok(response);
        }


    }
}
