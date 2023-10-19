using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet__rpg.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _conectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _conectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception ($"something went wrong");
            
        }
        public IDbConnection CreateConnection() => new SqlConnection(_conectionString);
    }
}