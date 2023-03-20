using AppiontmentBackEnd.Data;
using AppiontmentBackEnd.Models.PartialModels;
using AppiontmentBackEnd.ViewModels.Appiontment;

namespace AppiontmentBackEnd.Services.Appiontment
{
    public interface IAppiontmentService
    {
        MakeAppiontmentResponse MakeAppiontment(AppDbContext Db, MakeAppiontmentRequest request); 

        bool CheckIfAppionmentRequestIsValid(AppDbContext Db, CheckIfMakeAppiontmentRequestValid request); 

        List<AppiontmentListDisplayModel> GetALlAppiontmentByUserId(AppDbContext Db, UserIdPartial request) ;

        EditAppiontmentResponse EditAppiontment(AppDbContext Db, EditAppiontmentRequest request);

    }
}
