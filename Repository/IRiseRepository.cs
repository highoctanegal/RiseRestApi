using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseRestApi.Repository
{
    public interface IRiseRepository<U>
    {
        public Task<List<U>> GetAll();
        public Task<U> Get(int id);
        public Task<int> Put(int id, U model);
        public Task<int> Post(U model);
        public Task<int> Delete(int id);
        public bool Exists(int id);
    }
}
