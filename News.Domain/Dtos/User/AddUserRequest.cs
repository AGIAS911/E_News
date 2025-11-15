using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Anas_Abualsauod.News.Domain.Dtos.User;

public class AddUserRequest
{
    [Required]
    [MaxLength(100)]
    public required string UserName { get; set; }
    [Required]
    [MaxLength(100)]
    public required string FullName { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Email { get; set; }
    [Required]
    [MaxLength(200)]
    public required string Password { get; set; }

    public  IFormFile ProfileImage { get; set; }

}
