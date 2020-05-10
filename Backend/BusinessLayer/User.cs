using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class User
    {
        private List<string> columnNames;
        private bool _isLogged;
        private Board _board;
        private string email;
        private string password;
        private string nickname;

        public User(string email, string password, string nickname)
        {
            _isLogged = false;
            _board = new Board(email);
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
        public User() { }
        public User(UserWrapper user, Board board, List<string> columnNames)
        {
            _isLogged = user.IsLogged;
            _board = board;
            this.email = user.Email;
            this.password = user.Password;
            this.nickname = user.Nickname;
            this.columnNames = columnNames;
        }
        public List<string> ColumnNames { get => columnNames; set => columnNames = value; }
        public bool IsLogged
        {
            get => _isLogged;
            set
            {
                if(value != _isLogged)
                {
                    _isLogged = value;
                    save();
                }
                
            }
        }


        public Board Board
        {
            get => _board;
            set
            {
                if(_board != value)
                {
                    _board = value;
                    save();
                }
                   
            }
        }
        public string Email
        {
            get => email;
            set
            {
                if(email!=value)
                {
                    email = value;
                    save();
                }
                
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    save();
                }

            }
        }
        public string Nickname
        {
            get => nickname;
            set
            {
                if (nickname != value)
                {
                    nickname = value;
                    save();
                }

            }
        }

        public DataAccessLayer.UserWrapper ToDalObject()
        {
            return new DataAccessLayer.UserWrapper(email, password, nickname, _isLogged);
        }
        public void save()
        {            
            this.ToDalObject().Save();
        }
    }

}
