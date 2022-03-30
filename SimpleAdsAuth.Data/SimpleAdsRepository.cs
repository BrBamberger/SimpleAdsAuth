using System;
using System.Data.SqlClient;

namespace SimpleAdsAuth.Data
{
    public class SimpleAdsRepository
    {
        private string _connectionString;
        public SimpleAdsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

    }
}
