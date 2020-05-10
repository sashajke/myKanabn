using IntroSE.Kanban.Backend.BusinessLayer;
using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private UserController _UserController;
        public UserService(UserController controller)
        {
            _UserController = controller;
        }
        public Response Save()
        {
             _UserController.saveAll();
            return new Response();
        }
        public Response loadData()
        {
            _UserController.LoadData();
            return new Response();
        }
        public Response Register(string email, string password, string nickname)
        {
            Response toReturn;
            try
            {
                _UserController.Register(email, password, nickname);
                toReturn = new Response();
            }
            catch (Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }
        public Response<User> Login(string email, string password)
        {
            Response<User> toReturn;            try            {                BusinessLayer.User userToLogin = _UserController.Login(email, password);                toReturn = new Response<User>(new User(email, userToLogin.Nickname));            }            catch (Exception ee)            {                toReturn = new Response<User>("Email or password is incorrect");            }            return toReturn;        }
        public Response Logout(string email)
        {
            Response toReturn;
            try
            {
                _UserController.Logout(email);
                toReturn = new Response();
            }
            catch (Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }
    }
}
