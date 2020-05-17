using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.IO;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class BoardService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        BoardController boardController = null;
        public BoardService(UserController userController)
        {
            boardController = new BoardController(userController);
        }

        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            Response toReturn;
            try
            {
                boardController.LimitColumnTasks(email, columnOrdinal, limit);
                log.Debug($"Updated {email} column number {columnOrdinal} limit to be {limit}");
                toReturn = new Response();
            }
            catch(Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }
        public Response<Column> GetColumn(string email, string columnName)
        {
            Response<Column> toReturn;
            try
            {
                BusinessLayer.Column columnBusiness = boardController.GetColumn(email, columnName);
                log.Debug($"Retrieved column {columnName} from the board of {email} succesfully");
                Column columnService = new Column(columnBusiness);
                toReturn = new Response<Column>(columnService);
            }
            catch (Exception ee)
            {
                toReturn = new Response<Column>(ee.Message);
            }
            return toReturn;
        }
        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            Response<Column> toReturn;
            try
            {
                BusinessLayer.Column columnBusiness = boardController.GetColumn(email, columnOrdinal);
                log.Debug($"Retrieved column {columnOrdinal} from the board of {email} succesfully");
                Column columnService = new Column(columnBusiness);
                toReturn = new Response<Column>(columnService);
            }
            catch (Exception ee)
            {
                toReturn = new Response<Column>(ee.Message);
            }
            return toReturn;
        }
        public Response<Board> GetBoard(string email)
        {
            Response<Board> toReturn;
            try
            {
                Board boardService = new Board(boardController.GetBoard(email));
                log.Debug($"Retrieved the board of {email} succesfully");
                toReturn = new Response<Board>(boardService);
            }
            catch (Exception ee)
            {
                toReturn = new Response<Board>(ee.Message);
            }
            return toReturn;
        }

        public Response RemoveColumn(string email, int columnOrdinal)
        {
            Response toReturn;
            try
            {
                boardController.RemoveColumn(email, columnOrdinal);
                log.Debug($"Removed column {columnOrdinal} from the board of {email} succesfully");
                toReturn = new Response();
            }
            catch(Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }
        public Response<Column> AddColumn(string email, int columnOrdinal, string Name)
        {
            Response<Column> toReturn;
            try
            {
                Column columnService = new Column(boardController.AddColumn(email, columnOrdinal, Name));
                log.Debug($"Added column {Name} to the board of {email} in place number: {columnOrdinal} succesfully");

                toReturn = new Response<Column>(columnService);
            }
            catch(Exception ee)
            {
                toReturn = new Response<Column>(ee.Message);
            }
            return toReturn;
        }
        public Response<Column> MoveColumnRight(string email, int columnOrdinal)
        {
            Response<Column> toReturn;
            try
            {
                Column columnService = new Column(boardController.MoveColumnRight(email, columnOrdinal));
                log.Debug($"Moved column {columnOrdinal} right in the board of {email} succesfully");

                toReturn = new Response<Column>(columnService);
            }
            catch(Exception ee)
            {
                toReturn = new Response<Column>(ee.Message);
            }
            return toReturn;
        }

        public Response<Column> MoveColumnLeft(string email, int columnOrdinal)
        {
            Response<Column> toReturn;
            try
            {
                Column columnService = new Column(boardController.MoveColumnLeft(email, columnOrdinal));
                log.Debug($"Moved column {columnOrdinal} left in the board of {email} succesfully");

                toReturn = new Response<Column>(columnService);
            }
            catch (Exception ee)
            {
                toReturn = new Response<Column>(ee.Message);
            }
            return toReturn;
        }
    }

   
}
