using Dapper;
using System.Data;

namespace asdf_api.DapperExtensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Query<T>(this IDbConnection connection, Func<T> typeBuilder, string sql)
        {
            return connection.Query<T>(sql);
        }
    }
}
