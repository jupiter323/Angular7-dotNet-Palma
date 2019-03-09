using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PALMASoft.Configuration;
using PALMASoft.Web;

namespace PALMASoft.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class PALMASoftDbContextFactory : IDesignTimeDbContextFactory<PALMASoftDbContext>
    {
        public PALMASoftDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PALMASoftDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            PALMASoftDbContextConfigurer.Configure(builder, configuration.GetConnectionString(PALMASoftConsts.ConnectionStringName));

            return new PALMASoftDbContext(builder.Options);
        }
    }
}