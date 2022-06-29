using Microsoft.AspNetCore.Mvc;
using BookStore2.Repository;
using BookStore2.Models.Models;
using BookStore2.Models.ViewModels;

namespace BookStore2.Controllers;

[ApiController]

public class UserController : Controller
{
    UserRepository repository = new UserRepository();

    [HttpGet]
    [Route("GetUsers")]
    public IActionResult GetUser()
    {
        return Ok(repository.getUser());
    }

    [HttpPost]
    [Route("Login")]

    public IActionResult Login(LoginModel model)
    {
       User user= repository.Login(model);

        if(user==null)
        {
            return NotFound();
        }
        return Ok(user);


    }

    [HttpPost]
    [Route("Register")]
    public IActionResult Regiter(RegisterModel model)
    {
        User user = repository.Register(model);
        if(user==null)
        {
            return BadRequest();
        }
        return Ok(user);
    }
}
