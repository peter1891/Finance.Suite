using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Utilities.Database;

namespace Finance.Repository.Repository.Models
{
    public class AllocationRepository : Repository<AllocationModel>, IAllocationRepository
    {
        public AllocationRepository(DatabaseContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<AllocationModel>> GetAllocationsByAuthenticatedIdAsync(int id)
        {
            return new List<AllocationModel>();
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
