using CustomAuthorization.BLL.Enums;

namespace CustomAuthorization.BLL.DTOs
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserRole Role{get;set;}
    }
}
