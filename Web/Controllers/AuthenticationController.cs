using Anas_Abualsauod.News.Domain.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web.Controllers;

public class AuthenticationController : Controller
{
    private readonly UserAuthApi _user;

    public AuthenticationController(UserAuthApi userService)
    {
        _user = userService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> RegisterRequest([FromForm] AddUserRequest request)
    {
        using var formData = new MultipartFormDataContent();


        formData.Add(new StringContent(request.UserName ?? ""), "UserName");
        formData.Add(new StringContent(request.FullName ?? ""), "FullName");
        formData.Add(new StringContent(request.Email ?? ""), "Email");
        formData.Add(new StringContent(request.Password ?? ""), "Password");


        if (request.ProfileImage != null)
        {
            var streamContent = new StreamContent(request.ProfileImage.OpenReadStream());
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.ProfileImage.ContentType);
            formData.Add(streamContent, "ProfileImage", request.ProfileImage.FileName);
        }


        var result = await _user.RegisterRequest("Auth/Register", formData);

        return Redirect("Login");
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> LoginRequest(LoginUserRequest request)
    {

        var jsonRequest = JsonSerializer.Serialize(request);
        var result = await _user.LoginRequest("Auth/Login", jsonRequest);

        var loginResponse = JsonSerializer.Deserialize<LoginUserResponse>(result);
        HttpContext.Session.SetString("JWToken", loginResponse.accessToken);
        HttpContext.Session.SetInt32("UserId", loginResponse.id);

        return RedirectToAction("Index", "Home");
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> LogoutRequest()
    {
        await Task.CompletedTask;
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}