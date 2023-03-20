namespace AppiontmentBackEnd.ViewModels.Appiontment
{
    public class MakeAppiontmentRequest
    {
        public string? AppiontmentDate { get; set; }
        public string? AppiontmentTime { get; set; }
        public string? Description { get; set; }
        public List<string>? BodyPartsList { get; set; }
        public int UserId { get; set; }
    }
}
