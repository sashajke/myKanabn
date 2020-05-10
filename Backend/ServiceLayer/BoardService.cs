using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.IO;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class BoardService
    {
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
                toReturn = new Response<Board>(boardService);
            }
            catch (Exception ee)
            {
                toReturn = new Response<Board>(ee.Message);
            }
            return toReturn;
        }

    }
}
