using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDalFile:DalObject<UserDalFile>, IUserDAL
    {
        public UserDalFile(string email, string password, string nickname,bool isLogged)
        {
            IsLogged = isLogged;
            Email = email;
            Password = password;
            Nickname = nickname;
        }
        public UserDalFile() { }
        public string Email { get ; set ; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public bool IsLogged { get; set; }

        override
        public void Save()
        {
            ToJson(this, GetFileName(Email));
        }
        public bool Load(string email)
        {
            UserDalFile toLoad = null;
            try
            {
                toLoad = FromJson(GetFileName(email));
            }
            catch(Exception ee)
            {
                return false;
            }
            if (toLoad == null)
                return false;
            Email = toLoad.Email;
            Nickname = toLoad.Nickname;
            Password = toLoad.Password;
            IsLogged = toLoad.IsLogged;
            return true;
        }
        private string GetFileName(string email)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Users");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, email + ".json");
        }
    }
}
