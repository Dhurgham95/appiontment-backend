using AppiontmentBackEnd.Data;
using AppiontmentBackEnd.ViewModels.Register;
using Microsoft.EntityFrameworkCore;

namespace AppiontmentBackEnd.Services.Register
{
    public interface IUserRegisterService
    {
        RegisterResponse AddUserForRegistreation(AppDbContext Db,RegisterRequest req);
        RegisterResponse AddAdminForRegistreation(AppDbContext Db,RegisterRequest req);
        RegisterResponse AddDoctorForRegistreation(AppDbContext Db, RegisterRequest req);
    }
}
