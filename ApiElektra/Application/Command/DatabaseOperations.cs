using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiElektra.Application;
using ApiElektra.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApiElektra.Application.Command
{
    public class DatabaseOperations : IDatabaseOperations
    {

        private IConfiguration _config;

        public DatabaseOperations(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ResultJson> ExecuteReader(string Query)
        {
            var con = new SqlConnection(_config.GetConnectionString("Elektra"));
            try
            {
                var cmd = new SqlCommand(Query, con);
                var dt = new DataTable();
                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                return new ResultJson { Status = true, Result = sqlDatoToJson(dt) };
            }
            catch (Exception ex)
            {
                return new ResultJson { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResultJson> LoginUser(string userName,string password)
        {
            var con = new SqlConnection(_config.GetConnectionString("Elektra"));
            try
            {
                var cmd = new SqlCommand("SELECT * FROM [USER] WHERE KODU=@KODU AND SIFRESI=@SIFRESI AND WINTOM=1", con);
                cmd.Parameters.Add(new SqlParameter { DbType = DbType.String, ParameterName = "@KODU", Value = userName });
                cmd.Parameters.Add(new SqlParameter { DbType = DbType.String, ParameterName = "@SIFRESI", Value =password });

                var dt = new DataTable();
                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                    return new ResultJson { Status = true, Result = sqlDatoToJson(dt) };
                else
                    return new ResultJson { Status = false, Message = "User Not Found !" };
            }
            catch (Exception ex)
            {
                return new ResultJson { Status = false, Message = ex.Message };
            }
        }

        private object sqlDatoToJson(DataTable dataTable)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dataTable);
            return JSONString;
        }
    }
}
