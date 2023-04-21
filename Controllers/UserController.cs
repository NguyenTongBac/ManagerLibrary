using AutoMapper;
using Controllers.Dtos.Users;
using Controllers.IRepositories;
using Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;

namespace Controllers;

public class UserController : Controller
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _autoMapper;

    public UserController(IRepository<User> repository, IMapper autoMapper)
    {
        _repository = repository;
        _autoMapper = autoMapper;
    }

    public IActionResult Login(UserDto user)
    {
        var checkUser = _repository.GetQueryable().FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

        if(checkUser == null)
            return View();

        return RedirectToAction("Index", "Home", checkUser);
    }

    public IActionResult Register(UserCreateUpdateDto user)
    {
        var checkUser = _repository.GetQueryable().FirstOrDefault(x => x.UserName == user.UserName);

        if(checkUser != null)
            return View();

        return View("Login");
    }

//     public IActionResult Index(ConditionResultDto condition)
//     {
//         var user = _repository
//     }
// 
}