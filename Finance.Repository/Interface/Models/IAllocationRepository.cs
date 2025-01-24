using Finance.Models;

namespace Finance.Repository.Interface.Models
{
    public interface IAllocationRepository : IRepository<AllocationModel>
    {
        Task<IEnumerable<AllocationModel>> GetAllocationsByAuthenticatedIdAsync(int id);
    }
}
