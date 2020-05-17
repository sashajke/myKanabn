using IntroSE.Kanban.Backend.BusinessLayer;
using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private UserController _UserController;
        public UserService(UserController controller)
        {
            _UserController = controller;
        }
        //public Response Save()
        //{
        //     _UserController.saveAll();
        //    return new Response();
        //}
        public Response loadData()
        {
            _UserController.LoadData();
            return new Response();
        }

        public Response DeleteData()
        {
            try
            {
                _UserController.DeleteData();
                log.Debug("Deleted all data");
                return new Response();
            }
            catch(Exception ee)
            {
                return new Response(ee.Message);
            }
           
            
        }
        public Response Register(string email, string password, string nickname)
        {
            Response toReturn;
            try
            {
                _UserController.Register(email, password, nickname);
                log.Debug($"Registered {nickname} with email: {email} succesfully");
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
            Response<User> toReturn;            try            {                BusinessLayer.User userToLogin = _UserController.Login(email, password);
                log.Debug($"{email} Logged in succesfully");
                toReturn = new Response<User>(new User(email, userToLogin.Nickname));            }            catch (Exception ee)            {                toReturn = new Response<User>("Email or password is incorrect");            }            return toReturn;        }
        public Response Logout(string email)
        {
            Response toReturn;
            try
            {
                _UserController.Logout(email);
                log.Debug($"{email} Logged out succesfully");
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
