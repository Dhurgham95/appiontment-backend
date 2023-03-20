using AppiontmentBackEnd.Data;
using AppiontmentBackEnd.Models.PartialModels;
using AppiontmentBackEnd.ViewModels;
using AppiontmentBackEnd.ViewModels.Login;
using Microsoft.EntityFrameworkCore;

namespace AppiontmentBackEnd.Services.Login
{
    public interface IUserLoginService
    {
       LoginResponse LoginAndGetToken(AppDbContext Db, LoginRequest req);
        JwtDecodeModel DecodeToken(Token token);
    }
}
