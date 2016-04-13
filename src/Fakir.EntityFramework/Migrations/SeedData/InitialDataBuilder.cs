using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;

namespace Fakir.EntityFramework.Migrations.SeedData
{
    public class InitialDataBuilder
    {
        private readonly FakirDbContext _context;

        public InitialDataBuilder(FakirDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            _context.DisableAllFilters();
            new DefaultDataBuilder(_context).Build();
        }
    }
}
