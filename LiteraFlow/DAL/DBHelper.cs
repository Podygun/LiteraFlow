namespace LiteraFlow.Web.DAL
{
    public static class DBHelper
    {
        public static readonly string ConString = 
            "Username=postgres;Password=root;Server=localhost;Port=5432;Database=postgres";

        /// <summary>
        /// Modify DataBase
        /// </summary>
        /// <param name="sql">Just Insert, Update, Delete</param>
        /// <param name="model"></param>
        /// <returns>Numbers of rows affected</returns>
        public static async Task<int> ExecuteAsync(string sql, object model)
        {
            using (var con = new NpgsqlConnection(ConString))
            {
                await con.OpenAsync();
                var result = await con.ExecuteScalarAsync<int>(sql, model);
                return result;
            }
        }

        /// <summary>
        /// Modify DataBase with "returning"
        /// </summary>
        /// <param name="sql">Insert, Update, Delete with "returning"</param>
        /// <param name="model"></param>
        /// <returns>Returning value type T</returns>
        public static async Task<T> ExecuteScalarAsync<T>(string sql, object model)
        {
            using (var con = new NpgsqlConnection(ConString))
            {
                await con.OpenAsync();
                var result = await con.ExecuteScalarAsync<T>(sql, model);
                return result;
            }
        }

        /// <summary>
        /// Select single row
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using (var con = new NpgsqlConnection(ConString))
            {
                await con.OpenAsync();
                var result = await con.QueryFirstOrDefaultAsync<T>(sql, model);
                return result;
            }
        }

        /// <summary>
        /// Select collection of rows
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<IList<T>> QueryCollectionAsync<T>(string sql, object model)
        {
            using (var con = new NpgsqlConnection(ConString))
            {
                await con.OpenAsync();
                var result = await con.QueryAsync<T>(sql, model);
                return result.ToList();
            }
        }
    }
}
