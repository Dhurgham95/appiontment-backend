using AppiontmentBackEnd.Models;
using AppiontmentBackEnd.ViewModels.Register;

namespace AppiontmentBackEnd.Services.UserManagment.Doctors
{
    public interface IDoctorManagment
    {
        public List<User> GetAllDoctorsByAdminId(int adminId);
        public bool AddDoctorWithAdmin(RegisterRequest registerRequest);
        public bool UpdateDoctorWithAdmin(int adminId, RegisterRequest registerRequest);
        public bool DeleteDoctorWithAdmin(int adminId, int doctorId);
    }
}
