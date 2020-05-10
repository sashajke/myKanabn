﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class userControllerWrapper : DalObject<userControllerWrapper>
    {


        public List<UserWrapper> Users { get ; set ; }

        public userControllerWrapper()
        {
            Users = new List<UserWrapper>();
        }

        public void SaveAsSeperateFiles()
        {
            foreach (var item in Users)
            {
                item.Save();                
            }
        }
        public bool LoadAsSeperateFiles()
        {
            if (Users == null)
                Users = new List<UserWrapper>();
            Users.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    UserWrapper toLoad = FromJson<UserWrapper>(item);
                    Users.Add(toLoad);
                }
                catch(Exception ee)
                {

                }
            }
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
        private string GetDirectory()
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Users");
            return dir;
        }

        public override void Save()
        {
            //throw new NotImplementedException();
        }
    }
}
