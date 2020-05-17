﻿using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class UserDalDB : IUserDAL
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public bool IsLogged { get; set; }

        public UserDalDB(string email,string password,string nickname,bool isLogged)
        {
            this.Email = email;
            this.Password = password;
            this.Nickname = nickname;
            this.IsLogged = isLogged;
        }

        public bool Load(string email)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        public void Remove()
        {

        }
    }
}
