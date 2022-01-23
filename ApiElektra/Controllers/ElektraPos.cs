using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiElektra.Application;
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

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> ExecuteReader(string Query,string userName,string password)
        {
            var loginResult =await _databaseOp.LoginUser(userName, password);

            if(loginResult.Status)
            { 
            var result =await _databaseOp.ExecuteReader(Query);

            return Json(result);
            }else
            {
                return Json(loginResult);
            }
        }
    }
}
