using Dapper;
using System.Data;

namespace PCWel.Infra.DapperExtensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Query<T>(this IDbConnection connection, Func<T> typeBuilder, string sql)
        {
            return connection.Query<T>(sql);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, Func<T> typeBuilder, string sql)
        {
            return connection.QueryAsync<T>(sql);
        }
    }
}
