using PALMASoft.EntityFrameworkCore;

namespace PALMASoft.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly PALMASoftDbContext _context;

        public InitialHostDbBuilder(PALMASoftDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
