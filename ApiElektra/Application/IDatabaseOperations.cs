using ApiElektra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiElektra.Application
{
    public interface IDatabaseOperations
    {
        Task<ResultJson> ExecuteReader(string Query);

        Task<ResultJson> LoginUser(string userName, string password);
    }
}
