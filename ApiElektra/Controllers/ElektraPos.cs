using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiElektra.Application;
using System.Text;
using ApiElektra.Model;

namespace ApiElektra.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElektraPos : Controller
    {
        IDatabaseOperations _databaseOp;

        public ElektraPos(IDatabaseOperations databaseOp)
        {
            _databaseOp = databaseOp;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> ExecuteReader(ParamModel model)
        {
            var loginResult =await _databaseOp.LoginUser(model.userName, model.password);

            if(loginResult.Status)
            { 
            var result =await _databaseOp.ExecuteReader(model.Query);

            return Json(result);
            }else
            {
                return Json(loginResult);
            }
        }
    }
}
