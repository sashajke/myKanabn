

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct User
    {
        public readonly string Email;
        public readonly string Nickname;
        internal User(string email, string nickname)
        {
            this.Email = email;
            this.Nickname = nickname;
        }
        internal User(User copyUser)
        {
            this.Email = copyUser.Email;
            this.Nickname = copyUser.Nickname;
        }
    }
}
