
using AppiontmentBackEnd.Data;
using Microsoft.EntityFrameworkCore;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AppiontmentBackEnd.Services.Login;
using AppiontmentBackEnd.ViewModels.Login;
using AppiontmentBackEnd.ViewModels.Register;
using AppiontmentBackEnd.Services.Register;
using AppiontmentBackEnd.Models.PartialModels;
using AppiontmentBackEnd.Services.Appiontment;
using AppiontmentBackEnd.ViewModels.Appiontment;
using AppiontmentBackEnd.Models;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//connection String setting
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//authontication scheme

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};


//var contactInfo = new OpenApiContact()
//{
//    Name = "Dhurgham Zenki",
//    Email = "zenki.dark33@gmail.com",
//    Url = new Uri("https://")
//};

var license = new OpenApiLicense()
{
    Name = "Free License",
};

var info = new OpenApiInfo()
{
    Version = "V1",
    Title = "Appiontment Api Version One",
    Description = "Appiontment Api Version One",
  //  Contact = contactInfo,
    License = license
};

//add services 
builder.Services.AddSingleton<IUserLoginService, UserLoginService>();
builder.Services.AddSingleton<IUserRegisterService, UserRegisterService>();
builder.Services.AddSingleton<IAppiontmentService, AppiontmentService>();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", info);
    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(securityReq);
});

//auto mapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true; // check for now
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateLifetime = false, // In any other application other then demo this needs to be true,
        ValidateIssuerSigningKey = true
    };
});

//builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();


app.MapPost("adminregister", [Authorize(Roles = "SuperAdmin, Admin")] (AppDbContext Db, RegisterRequest req, IUserRegisterService registerService)
    => RegisterAdminAction(Db, req, registerService));

app.MapPost("doctorregister", [Authorize(Roles = "SuperAdmin, Admin")] (AppDbContext Db, RegisterRequest req, IUserRegisterService registerService)
    => RegisterDoctorAction(Db, req, registerService));


app.MapPost("/register", [AllowAnonymous]
    (AppDbContext Db, RegisterRequest req, IUserRegisterService registerService)
    => RegisterAction( Db,  req,  registerService));

app.MapGet("/", [Authorize(Roles = "User")] () => "Hello World!");

app.MapPost("/getuserrolesfromid", [Authorize(Roles = "User")] () => "Hello World!");

app.MapPost("/login" , [AllowAnonymous]  (AppDbContext Db, LoginRequest req, IUserLoginService loginService ) => LoginAction(Db, req, loginService));

app.MapPost("/validatetoken", (Token token ,IUserLoginService service) => DecodeAction(token, service));

app.MapPost("/addappiontment", [Authorize](AppDbContext Db, MakeAppiontmentRequest req, IAppiontmentService appService) => MakeAppiontmentAction(Db, req, appService));

app.Map("/getallapptsbyuserid",  (AppDbContext Db, UserIdPartial userId, IAppiontmentService appService) => GetAppApptsActionByUserId(Db, userId, appService));

app.MapPost("/editappiontment", [Authorize] (AppDbContext Db, EditAppiontmentRequest req, IAppiontmentService appService) => EditAppiontmentAction(Db, req, appService));

app.MapPost("/checkifapptreqisvalid", [Authorize] (AppDbContext Db, CheckIfMakeAppiontmentRequestValid req, IAppiontmentService appService) => CheckAppiontmentRequestValidationAction(Db, req, appService));


IResult RegisterAction(AppDbContext Db, RegisterRequest req, IUserRegisterService registerService)
{
    var result = registerService.AddUserForRegistreation(Db,req);
    if(result.IsRegisterSucceed)
    {
        return Results.Ok(result);
    } 
    else
    {
        return Results.Unauthorized();
    }

}

IResult RegisterAdminAction(AppDbContext Db, RegisterRequest req, IUserRegisterService registerService)
{
    var result = registerService.AddAdminForRegistreation(Db, req);
    if (result.IsRegisterSucceed)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.Unauthorized();
    }

}

IResult RegisterDoctorAction(AppDbContext Db, RegisterRequest req, IUserRegisterService registerService)
{
    var result = registerService.AddDoctorForRegistreation(Db, req);
    if (result.IsRegisterSucceed)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.Unauthorized();
    }

}


IResult LoginAction(AppDbContext Db, LoginRequest req , IUserLoginService loginService)
{
    var result = loginService.LoginAndGetToken(Db,req);
    if(result.IsLoginSucceed)
    {
        return Results.Ok(result);

    } 
    else
    {
        return Results.Unauthorized();
    }
    

}

IResult DecodeAction(Token token, IUserLoginService service)
{
    var result = service.DecodeToken(token);
    if(result != null)
    {
        return Results.Ok(result);
    } 
    else
    {
        return Results.NotFound();
    }
} 

IResult MakeAppiontmentAction(AppDbContext Db,MakeAppiontmentRequest req, IAppiontmentService appService)
{
    var result = appService.MakeAppiontment(Db,req); 

    if(result.IsSucceed)
    {
        return Results.Ok(result);
    } 
    else
    {
        return Results.NotFound();
    }
}

IResult GetAppApptsActionByUserId(AppDbContext Db, UserIdPartial userId , IAppiontmentService appService)
{
    var result = appService.GetALlAppiontmentByUserId(Db, userId);

    if(result != null)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound();
    }
} 

IResult EditAppiontmentAction(AppDbContext Db, EditAppiontmentRequest req, IAppiontmentService appService)
{
    var ressult = appService.EditAppiontment(Db, req);
    if(ressult != null || ressult.IsSucceed == true) 
    {
        return Results.Ok(ressult);

    }
    else
    {
        return Results.NotFound();
    }
}


IResult CheckAppiontmentRequestValidationAction(AppDbContext Db, CheckIfMakeAppiontmentRequestValid req, IAppiontmentService appService)
{
    var result = appService.CheckIfAppionmentRequestIsValid(Db, req);  
    if(result != null )
    {
        return Results.Ok(result);  
    } 
    else
    {
        return Results.BadRequest();
    }
}
app.Run();
