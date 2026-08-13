using Microsoft.Data.SqlClient;

namespace WebMusic.Data;

// Cấu hình kết nối trung tâm: 1 connection string từ appsettings.
public static class Db
{
    public static string ConnectionString { get; private set; } = string.Empty;

    public static void Configure(IConfiguration config) =>
        ConnectionString = config.GetConnectionString("MusicDb")!;

    public static SqlConnection CreateConnection() => new(ConnectionString);
}