using AppiontmentBackEnd.Models;
using AppiontmentBackEnd.ViewModels.Register;

namespace AppiontmentBackEnd.Services.UserManagment.Doctors
{
    public class DoctorManagment : IDoctorManagment
    {
        public bool AddDoctorWithAdmin(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDoctorWithAdmin(int adminId, int doctorId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllDoctorsByAdminId(int adminId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDoctorWithAdmin(int adminId, RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
