using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace PageGenerator.Util
{
    public class Util
    {
        private ConnectionConfig config;

        public Util()
        {
            this.config = new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.AppSettings["conn"],
                DbType = "SqlServer",
            };
        }
        string[] GetExcludeKeywords()
        {
            var keywords = ConfigurationManager.AppSettings["excludePrfix"];
            return keywords?.Trim(',').Split(',');
        }

        public IEnumerable<DbTableInfo> LoadTables()
        {
            using (SqlSugarClient client = new SqlSugarClient(config))
            {
                var kws = GetExcludeKeywords();
                return client.DbMaintenance.GetTableInfoList()?.Where(p => !kws.Any(t => p.Name.ToLower().StartsWith(t.ToLower())));
            }
        }

        public List<DbColumnInfo> GetColumns(string tableName)
        {
            using (SqlSugarClient client = new SqlSugarClient(config))
            {
                return client.DbMaintenance.GetColumnInfosByTableName(tableName);
            }
        }
    }
}
