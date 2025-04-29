namespace BugTicketingSystem.BL
{

    public class UsersRegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; } = UserRole.User;

    }



}
