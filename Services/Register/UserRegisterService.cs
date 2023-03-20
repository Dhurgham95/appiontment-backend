using AppiontmentBackEnd.Data;
using AppiontmentBackEnd.Models;
using AppiontmentBackEnd.ViewModels.Register;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppiontmentBackEnd.Services.Register
{
    public class UserRegisterService : IUserRegisterService
    {
        //  private readonly IMapper _mapper;
        //public UserRegisterService(IMapper mapper)
        //{
        // //   _mapper = mapper;

        //}
        //private readonly AppDbContext _db;
        //public UserRegisterService(AppDbContext db)
        //{
        //    _db = db;

        //}

        public RegisterResponse AddAdminForRegistreation(AppDbContext Db, RegisterRequest req)
        {
            RegisterResponse res = new();
            Role role = new();
            UserRole userRole = new();
            res.RolesNames = new();
            var userToRegister = Db.Users.Where(u => u.UserName == req.UserName
                                                     || u.Email == req.Email
                                                     || u.PhoneNumber == req.PhoneNumber).FirstOrDefault();
            if (userToRegister != null)
            {
                res.IsRegisterSucceed = false;
                res.PhoneNumber = String.Empty;
                res.Email = String.Empty;
                res.CreatedAt = String.Empty;
                res.UserName = String.Empty;
                res.FullName = String.Empty;
                res.ErrorMessage = "User Already Exist with this data";
                return res;


            }

            User userToAdd = new();
            userToAdd.UserName = req.UserName;
            userToAdd.FullName = req.FullName;
            userToAdd.PhoneNumber = req.PhoneNumber;
            userToAdd.Email = req.Email;
            userToAdd.Gender = req.Gender;
            userToAdd.PhoneNumber = req.PhoneNumber;
            userToAdd.Address = req.Address;
            userToAdd.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd | hh:mm:ss");
            userToAdd.IsActive = true;
            userToAdd.Password = req.Password;
            Db.Users.Add(userToAdd);
            Db.SaveChanges();
            var addedUserId = userToAdd.Id;
            foreach (var u in req.RolesNames)
            {
                

                //get role id 
                var addedUserRole = Db.Roles.Where(r => r.Name == u.RoleName).FirstOrDefault();
                if (addedUserRole == null)
                {

                    res.IsRegisterSucceed = false;
                    res.PhoneNumber = String.Empty;
                    res.Email = String.Empty;
                    res.CreatedAt = String.Empty;
                    res.UserName = String.Empty;
                    res.FullName = String.Empty;
                    res.ErrorMessage = "User Roles donot exist";

                    return res;



                }

                var addedUserRoleId = addedUserRole.Id;
                //add description 
                u.RoleDesc = addedUserRole.Description;
                userRole.UserId = addedUserId;
                userRole.RoleId = addedUserRoleId;

                Db.UserRoles.Add(userRole);
                Db.SaveChanges();
                //adding response roles 



                res.RolesNames.Add(u);

            }


            res.IsRegisterSucceed = true;
            res.PhoneNumber = userToAdd.PhoneNumber;
            res.Email = userToAdd.Email;
            res.CreatedAt = userToAdd.CreatedAt;
            res.UserName = userToAdd.UserName;
            res.FullName = userToAdd.FullName;
            res.Id = addedUserId;
            res.IsActive = userToAdd.IsActive;
            res.Gender = userToAdd.Gender;
            res.Address = userToAdd.Address;

            res.ErrorMessage = String.Empty;

            return res;

        }

        public RegisterResponse AddDoctorForRegistreation(AppDbContext Db, RegisterRequest req)
        {
            RegisterResponse res = new();
            Role role = new();
            UserRole userRole = new();
            res.RolesNames = new();
            var userToRegister = Db.Users.Where(u => u.UserName == req.UserName
                                                     || u.PhoneNumber == req.PhoneNumber).FirstOrDefault();
            if (userToRegister != null)
            {
                res.IsRegisterSucceed = false;
                res.PhoneNumber = String.Empty;
                res.Email = String.Empty;
                res.CreatedAt = String.Empty;
                res.UserName = String.Empty;
                res.FullName = String.Empty;
                res.ErrorMessage = "User Already Exist with this data";
                return res;


            }

            User userToAdd = new();
            userToAdd.UserName = req.UserName;
            userToAdd.FullName = req.FullName;
            userToAdd.PhoneNumber = req.PhoneNumber;
            userToAdd.Gender = req.Gender;
            userToAdd.PhoneNumber = req.PhoneNumber;
            userToAdd.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd | hh:mm:ss");
            userToAdd.IsActive = true;
            userToAdd.Password = req.Password;
            userToAdd.AdderId = req.Id;
            userToAdd.Specialization = req.Specialization;
            Db.Users.Add(userToAdd);
            Db.SaveChanges();
            var addedUserId = userToAdd.Id;
            foreach (var u in req.RolesNames)
            {


                //get role id 
                var addedUserRole = Db.Roles.Where(r => r.Name == u.RoleName).FirstOrDefault();
                if (addedUserRole == null)
                {

                    res.IsRegisterSucceed = false;
                    res.PhoneNumber = String.Empty;
                    res.Email = String.Empty;
                    res.CreatedAt = String.Empty;
                    res.UserName = String.Empty;
                    res.FullName = String.Empty;
                    res.ErrorMessage = "User Roles donot exist";

                    return res;



                }

                var addedUserRoleId = addedUserRole.Id;
                //add description 
                u.RoleDesc = addedUserRole.Description;
                userRole.UserId = addedUserId;
                userRole.RoleId = addedUserRoleId;

                Db.UserRoles.Add(userRole);
                Db.SaveChanges();
                //adding response roles 



                res.RolesNames.Add(u);

            }


            res.IsRegisterSucceed = true;
            res.PhoneNumber = userToAdd.PhoneNumber;
            res.Email = userToAdd.Email;
            res.CreatedAt = userToAdd.CreatedAt;
            res.UserName = userToAdd.UserName;
            res.FullName = userToAdd.FullName;
            res.Id = addedUserId;
            res.IsActive = userToAdd.IsActive;
            res.Gender = userToAdd.Gender;
            res.Address = userToAdd.Address;

            res.ErrorMessage = String.Empty;

            return res;


        }

        public RegisterResponse AddUserForRegistreation(AppDbContext Db , RegisterRequest req)
        {
            RegisterResponse res = new();
            Role role = new();
            UserRole userRole = new();
            res.RolesNames = new();
            var userToRegister = Db.Users.Where(u => u.UserName == req.UserName
                                                     || u.Email == req.Email
                                                     || u.PhoneNumber == req.PhoneNumber).FirstOrDefault();
            if(userToRegister != null)
            {
                res.IsRegisterSucceed = false;
                res.PhoneNumber = String.Empty;
                res.Email = String.Empty;
                res.CreatedAt = String.Empty;
                res.UserName = String.Empty;
                res.FullName = String.Empty;
                res.ErrorMessage = "User Already Exist with this data";
                return res;


            }

            User userToAdd = new();
            userToAdd.UserName = req.UserName;
            userToAdd.FullName = req.FullName;
            userToAdd.PhoneNumber = req.PhoneNumber;
            userToAdd.Email = req.Email;
            userToAdd.Gender = req.Gender;
            userToAdd.PhoneNumber = req.PhoneNumber;
            userToAdd.Address = req.Address;
            userToAdd.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd | hh:mm:ss");
            userToAdd.IsActive = true;
            userToAdd.Password = req.Password;
            Db.Users.Add(userToAdd);
            Db.SaveChanges();
            var addedUserId = userToAdd.Id;
            foreach (var u in req.RolesNames)
            {
                //var checkIfRoleIsExsists = Db.Roles.Where(r => r.Name == u.RoleName).FirstOrDefault(); 
                //if(checkIfRoleIsExsists != null)
                //{
                //    var UsrRoleId = checkIfRoleIsExsists.Id;

                //    userRole.UserId = addedUserId;
                //    userRole.RoleId = UsrRoleId;

                //    Db.UserRoles.Add(userRole);
                //    Db.SaveChanges();
                //    //adding response roles 

                //    res.RolesNames = new();
                //    res.RolesNames.Add(u);
                //    continue;
                //}
                //role.Name = u.RoleName;
                //role.Description = u.RoleDesc;
                //Db.Roles.Add(role);
                //Db.SaveChanges();

                //get role id 
                var addedUserRole = Db.Roles.Where(r => r.Name == u.RoleName).FirstOrDefault(); 
                if(addedUserRole == null)
                {
                    
                    res.IsRegisterSucceed = false;
                    res.PhoneNumber = String.Empty;
                    res.Email = String.Empty;
                    res.CreatedAt = String.Empty;
                    res.UserName = String.Empty;
                    res.FullName = String.Empty;
                    res.ErrorMessage = "User Roles donot exist";

                    return res;
                    
                   

                }

                var addedUserRoleId = addedUserRole.Id;
                //add description 
                u.RoleDesc = addedUserRole.Description;
                userRole.UserId = addedUserId;
                userRole.RoleId = addedUserRoleId;

                Db.UserRoles.Add(userRole);
                Db.SaveChanges();
                //adding response roles 

                
                
                res.RolesNames.Add(u);

            }

           
            res.IsRegisterSucceed = true;
            res.PhoneNumber = userToAdd.PhoneNumber;
            res.Email = userToAdd.Email;
            res.CreatedAt = userToAdd.CreatedAt ;
            res.UserName = userToAdd.UserName;
            res.FullName = userToAdd.FullName;
            res.Id = addedUserId;
            res.IsActive = userToAdd.IsActive;
            res.Gender = userToAdd.Gender;
            res.Address = userToAdd.Address;

            res.ErrorMessage = String.Empty;

            return res;


            //assing Current database context



        }
    }
}
