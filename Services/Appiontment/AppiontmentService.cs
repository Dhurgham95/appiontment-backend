using AppiontmentBackEnd.Data;
using AppiontmentBackEnd.Helpers;
using AppiontmentBackEnd.Models;
using AppiontmentBackEnd.Models.PartialModels;
using AppiontmentBackEnd.ViewModels.Appiontment;

namespace AppiontmentBackEnd.Services.Appiontment
{
    public class AppiontmentService : IAppiontmentService
    {
        public bool CheckIfAppionmentRequestIsValid(AppDbContext Db, CheckIfMakeAppiontmentRequestValid request)
        {
            List<Appionetment> appionetments = new();
            List<int> idds = new();
            var userAppiontmentlst = Db.UserAppionetments.Where(a => a.UserId == request.UserId).ToList();
            foreach(var item in userAppiontmentlst)
            {
                idds.Add(item.AppionetmentId);
                
            }
            //get all appiontments date for user 
            appionetments = Db.Appionetments.Where(a => idds.Contains(a.Id)).ToList(); 
            foreach(var appt in appionetments)
            {
                var dateApptAsString = appt.DateOfAppiontment.Split("||").First(); 
                var dateApptAsDate = DateTime.Parse(dateApptAsString);
                if(dateApptAsDate.Day == DateTime.Now.Day)
                {
                   
                    return true;
                }

                
            }

            // var checkkdate = appionetments.Where(a => a.DateOfAppiontment) 

            return false;
            //appionetments = appts.Where(a => a.Id.ToString().ci)
           

        }

        public EditAppiontmentResponse EditAppiontment(AppDbContext Db, EditAppiontmentRequest request)
        {
            EditAppiontmentResponse res = new();
            var apptoEdit = Db.Appionetments.Where(a => a.Id == request.AppId).FirstOrDefault();
            if (apptoEdit != null)
            {
                apptoEdit.DateOfAppiontment = request.AppiontmentDate + "||" + request.AppiontmentTime;
                Db.SaveChanges();
                res.ErrorMessage = String.Empty;
                res.IsSucceed = true;
                return res; 

            }
            else
            {

                res.ErrorMessage = String.Empty;
                res.IsSucceed = true;
                return res;
            }


        }

        public List<AppiontmentListDisplayModel> GetALlAppiontmentByUserId(AppDbContext Db, UserIdPartial request)
        {
            List<AppiontmentListDisplayModel> lst = new();
            List<int> idds = new();
            var lstAppiontments = Db.UserAppionetments.Where(a => a.UserId == request.UserId).OrderByDescending(a => a.AppionetmentId).ToList();
            StatusNameMap statusNameMap = new StatusNameMap();
           foreach (var item in lstAppiontments)
            {
                idds.Add(item.AppionetmentId);
            }

             var finallst = Db.Appionetments.Where(a => idds.Contains(a.Id)).OrderByDescending(a => a.Id).ToList();

          
          
          
            foreach (var a in finallst)
            {
              
                AppiontmentListDisplayModel model = new();
                model.UserId = request.UserId;
                model.AppiontmentId = a.Id;
                model.DateOfAppiontment = a.DateOfAppiontment.Substring(0, a.DateOfAppiontment.IndexOf("||"));
                model.TimeOfAppiontment = a.DateOfAppiontment.Substring(a.DateOfAppiontment.IndexOf("||") + "||".Length );
                model.Status = statusNameMap.MappStatusName[Db.Statuses.Where(s => s.Id == a.StatusId).FirstOrDefault().Name];
                model.Description = a.Description;
                lst.Add(model);

            }

            return lst;

        }

        public MakeAppiontmentResponse MakeAppiontment(AppDbContext Db, MakeAppiontmentRequest request)
        {
            MakeAppiontmentResponse mkappres = new();
            string? bodyPartsAsString = String.Empty;;
            var apppintementtoMake = new Appionetment();
            var appiontmentUser = new UserAppionetment();
            apppintementtoMake.DateOfAppiontment = request.AppiontmentDate + "||" + request.AppiontmentTime;
            apppintementtoMake.StatusId = 1;
            apppintementtoMake.Description = request.Description;

            foreach(var part in request.BodyPartsList!)
            {
                bodyPartsAsString = bodyPartsAsString + "||" + part;

            }

            apppintementtoMake.BodyParts = bodyPartsAsString;
          
            Db.Appionetments.Add(apppintementtoMake);
            Db.SaveChanges();
            appiontmentUser.UserId = request.UserId;
            appiontmentUser.AppionetmentId = apppintementtoMake.Id;
            // appiontmentUser.Appionetment = apppintementtoMake;

            Db.UserAppionetments.Add(appiontmentUser);

            Db.SaveChanges();

            mkappres.IsSucceed = true;
            mkappres.ErrorMessage = String.Empty;

            return mkappres;


        }
    }
}
