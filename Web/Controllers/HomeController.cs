using Anas_Abualsauod.News.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web.Controllers;

public class HomeController : BaseController
{
    public HomeController( IHttpContextAccessor httpContextAccessor, UserApi user)
        : base( httpContextAccessor, user)
    {
    }




    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {

        var user = await GetCurrentUserAsync();

        return View(user);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> AddNews()
    {

        var user = await GetCurrentUserAsync();
        await Task.CompletedTask;
        return View(user);
    }

    



}
