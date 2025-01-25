using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Utilities.Database;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository.Repository.Models
{
    public class AllocationRepository : Repository<AllocationModel>, IAllocationRepository
    {
        public AllocationRepository(DatabaseContext context) 
            : base(context)
        {
        }

        public override async Task<AllocationModel> GetByIdAsync(int id)
        {
            return await DatabaseContext.AllocationModels
                .Include(a => a.Account)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AllocationModel>> GetAllocationsByAuthenticatedIdAsync(int id)
        {
            return await DatabaseContext.AllocationModels
                .Where(a => a.Account.UserId == id)
                .ToListAsync();
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
