using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.EntityFrameworkCore
{
    public static class PALMASoftDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PALMASoftDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PALMASoftDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}