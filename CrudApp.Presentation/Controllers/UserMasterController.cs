using CrudApp.DataAccess;
using CrudApp.DataModel;
using CrudApp.Logic.UserMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CrudApp.Presentation.Controllers
{
    public class UserMasterController : Controller
    {
        FunctionClass fnobj = new FunctionClass();

        // GET: UserMaster
        public ActionResult Index()
        {
            return View();
        }

        public string List(UserMasterModel model)
        {
            try
            {

                using (UserMasterLogic logic = new UserMasterLogic())
                {
                    return fnobj.JSONResponse("200", "success", logic.GetList(model), "");
                }
            }
            catch (Exception ex)
            {
                return fnobj.JSONResponse("404", ex.Message, null, ex.ToString());
            }
        }


        public string SaveUpdate(UserMasterModel model)
        {
            try
            {
                using (UserMasterLogic logic = new UserMasterLogic())
                {
                    return fnobj.JSONResponse("200", "success", logic.SaveAndUpdate(model), "");
                }
            }
            catch (Exception ex)
            {
                return fnobj.JSONResponse("404", ex.Message, null, ex.ToString());
            }
        }

        public string Edit(UserMasterModel model)
        {
            try
            {
                using (UserMasterLogic logic = new UserMasterLogic())
                {
                    return fnobj.JSONResponse("200", "success", logic.GetListById(model), "");
                }
            }
            catch (Exception ex)
            {
                return fnobj.JSONResponse("404", ex.Message, null, ex.ToString());
            }
        }

        public string Delete(UserMasterModel model)
        {
            try
            {
                using (UserMasterLogic logic = new UserMasterLogic())
                {
                    return fnobj.JSONResponse("200", "success", logic.Delete(model), "");
                }
            }
            catch (Exception ex)
            {
                return fnobj.JSONResponse("404", ex.Message, null, ex.ToString());
            }
        }
    }
}