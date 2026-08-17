using FreeSql;
using System.Diagnostics;

namespace EasyAuthenticator.Ext
{
    /// <summary>
    /// 本地SQLite数据库访问（IFreeSql单例）
    /// </summary>
    public static class LocalDb
    {
        /// <summary>
        /// FreeSql实例（首次访问时自动建库建表，SQLite默认ReadWriteCreate模式会自动创建base.db文件）
        /// </summary>
        public static readonly IFreeSql Fsql = new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, "Data Source=base.db")
            .UseNoneCommandParameter(true)//无参化
            .UseMonitorCommand(cmd => Debug.WriteLine($"【SQL】{cmd.CommandText.Trim()}")) // 打印SQL到调试输出
            .Build();

        static LocalDb()
        {
            //确保PasswordInfo表存在
            Fsql.Ado.ExecuteNonQuery(@"CREATE TABLE IF NOT EXISTS PasswordInfo (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Pwd TEXT NOT NULL,
    Createtime DATETIME NOT NULL,
    IsDelete INTEGER NOT NULL DEFAULT 0
);");
        }
    }
}
