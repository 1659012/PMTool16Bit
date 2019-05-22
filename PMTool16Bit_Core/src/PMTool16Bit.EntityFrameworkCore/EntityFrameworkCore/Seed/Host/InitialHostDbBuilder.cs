namespace PMTool16Bit.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly PMTool16BitDbContext _context;

        public InitialHostDbBuilder(PMTool16BitDbContext context)
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
