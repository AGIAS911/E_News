using Anas_Abualsauod.News.Domain.Dtos.User;
using Anas_Abualsauod.News.Domain.Entities;
using Anas_Abualsauod.News.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Web.Controllers;

public class BaseController : Controller
{
    protected readonly UserApi _user;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public BaseController( IHttpContextAccessor httpContextAccessor, UserApi user)
    {
        _httpContextAccessor = httpContextAccessor;
        _user = user;
    }

    protected async Task<UserInfo?> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
        if (userId == null) return null;

        var userJson = await _user.GetUserById("User/GetById", userId.Value);
        return JsonSerializer.Deserialize<UserInfo>(userJson);
    }



    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = await GetCurrentUserAsync();
        if (user != null)
        {
            ViewData["CurrentUserName"] = user.userName;
            ViewData["CurrentUserFullName"] = user.fullName;
            ViewData["CurrentUserImage"] = string.IsNullOrEmpty(user.imagePath)
                ? "/images/default-profile.png"
                : $"https://localhost:7041{user.imagePath}";
        }

        await next();
    }
}
