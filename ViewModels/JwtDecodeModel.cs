namespace AppiontmentBackEnd.ViewModels
{
    public class JwtDecodeModel
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public List<RoleJwt> JwtRoles { get; set; }
    }
}
