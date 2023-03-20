using AppiontmentBackEnd.Data;
using AppiontmentBackEnd.Models;
using AppiontmentBackEnd.Models.PartialModels;
using AppiontmentBackEnd.ViewModels;
using AppiontmentBackEnd.ViewModels.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppiontmentBackEnd.Services.Login
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IConfiguration _configuration;
        public UserLoginService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public JwtDecodeModel DecodeToken(Token token)
        {
            try
            {
                List<string> testt = new();
                JwtDecodeModel jwtDecode = new();
                List<RoleJwt> roleJwts = new();

                //var toekn = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InRlc3QiLCJpZCI6IjUiLCJwaG9uZW51bWJlciI6IjY2NjY2IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIlVzZXIiLCJBZG1pbiIsIkVtcGxveWVlIl0sImV4cCI6MTY2MDM4OTk3MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA4OC8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDg4LyJ9.5rCE1rA7shZ5OdcoxY7qoK0MEKCixpx0o3SL9jGKWp0";
                //var token = "[encoded jwt]";
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };

                var handler = new JwtSecurityTokenHandler();

                var claims = handler.ValidateToken(token.TokenString, validations, out var tokenSecure);
                foreach (var cc in claims.Claims.Where(c => c.Type == ClaimTypes.Role))
                {
                    RoleJwt roleJwt = new();
                    roleJwt.RoleName = cc.Value;
                    roleJwts.Add(roleJwt);

                    // testt.Add(cc.Value); 
                }

                var userNameClaims = claims.FindFirst(c => c.Type == "username").Value;
                var idClaims = claims.FindFirst(c => c.Type == "id").Value;
                var phoneNumberClaims = claims.FindFirst(c => c.Type == "phonenumber").Value;
                jwtDecode.JwtRoles = roleJwts;
                jwtDecode.UserName = userNameClaims;
                jwtDecode.PhoneNumber = phoneNumberClaims;
                jwtDecode.Id = int.Parse(idClaims);
                return jwtDecode;
            }
            catch (Exception ex)
            {



                return new JwtDecodeModel();

            }
           // return claims.Claims.ToList();
                //FindAll(claim => claim.Type == ClaimTypes.Role);
            //return claims.FindFirst(claim => claim.Type == "username").Value;

            //var jwtSecurityToken = handler.ReadJwtToken(toekn);
            // return jwtSecurityToken.RawData;
        }


        
        public  LoginResponse LoginAndGetToken(AppDbContext Db, LoginRequest req)
        {
            LoginResponse res = new();
            List<Role> rolesList = new();
            //check if user exist in database 
            var loginUser = Db.Users.Where(u => u.UserName == req.UserName && u.Password == req.Password)
                                                 .FirstOrDefault();
            if(loginUser == null )
            {
                res.IsLoginSucceed = false;
                res.Token = String.Empty;
                res.UserId = -1;
                res.ErrorMessage = "User With user Name and password does not exists";

                return res;
                
            }

    
            //   if (req.UserName == "user" && req.Password == "password")
            //  { 

            // claim setting check 
            var claims = new List<Claim>();
            claims.Add(new Claim("username", req.UserName));
            claims.Add(new Claim("id", loginUser.Id.ToString()));
            claims.Add(new Claim("phonenumber", loginUser.PhoneNumber));
            //  claims.Add(new Claim("email", loginUser.Email));
            //  claims.Add(new Claim("role", loginUser.UserRoles.))
            //get roles of logedInUser 

            var userAndRoles = Db.UserRoles.Where(u => u.UserId == loginUser.Id).ToList();
            foreach(var userrole in userAndRoles)
            {
                var userRoles = Db.Roles.Where(r => r.Id == userrole.RoleId).FirstOrDefault();
                rolesList.Add(userRoles);
            }
           
            // Add roles as multiple claims
            foreach (var role in rolesList)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            var issuer = _configuration.GetValue<string>("jwt:Issuer");
            var audience = _configuration.GetValue<string>("Jwt:Audience");
            var securityKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
                var credentials = new SigningCredentials(securityKey,
                              SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    signingCredentials: credentials,


                    //adding additional info to this token 
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7)
                    
                    );

                var tokenHandler = new JwtSecurityTokenHandler();
                var stringToken = tokenHandler.WriteToken(token);

                res.UserId = loginUser.Id;
                res.Token = stringToken;
                res.IsLoginSucceed = true;
                res.ErrorMessage = String.Empty;
                //return  Task.FromResult<LoginResponse>(res).Result;
                //return await Task.FromResult<LoginResponse>(res);
                return res;
          //  }
            //else
            //{
            //    res.UserId = -1;
            //    res.Token = String.Empty;
            //    res.IsLoginSucceed = false;
            //    res.ErrorMessage = "Not Valid Credintial";
            //    // return  Task.FromResult<LoginResponse>(res).Result;
            //    // return await Task.FromResult<LoginResponse>(res);
            //    return res;
            //}
        }
    }
}
