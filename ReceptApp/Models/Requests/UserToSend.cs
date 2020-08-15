namespace ReceptApp.Models.Requests
{
    public class UserToSend
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NickName { get; private set; }
        public string Email { get; private set; }

        public UserToSend(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            NickName = user.NickName;
            Email = user.Email;
        }
    }
}
