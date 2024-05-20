using FlavorCart.Models;
using FlavorCart.Enums;

using Google.Cloud.Firestore;

namespace FlavorCart.Repositories
{
    public class ListsRepository
    {
        private readonly BaseRepository<Lists> _repository;
        public ListsRepository()
        {
            // This should be injected - This is just an example.
            _repository = new BaseRepository<Lists>(Collection.Lists);
        }

        public async Task<List<Lists>> GetAllAsync() => await _repository.GetAllAsync<Lists>();

        public async Task<Lists?> GetAsync(Lists entity) => (Lists?)await _repository.GetAsync(entity);

        public async Task<Lists> AddAsync(Lists entity) => await _repository.AddAsync(entity);

        public async Task<Lists> UpdateAsync(Lists entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Lists entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Lists>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Lists>(query);

        // This is specific to Lists.
        
        public async Task<List<Lists>> GetListsByUser(string userId)
        {
             var query = _repository._firestoreDb.Collection(Collection.Lists.ToString())
                .WhereEqualTo("UserId", userId);
             return await this.QueryRecordsAsync(query);
        }
        // metodo para borrar una lista vinculada al id de usuario, controlar si la lista es publica o no
        public async Task DeleteListByUser(string userId, bool eliminarTodasLasListas)
        {
            var query = _repository._firestoreDb.Collection(Collection.Lists.ToString())
                .WhereEqualTo("UserId", userId);
            if (!eliminarTodasLasListas)
            {
                query = query.WhereEqualTo("IsPublic", false);
            }
            var lists = await this.QueryRecordsAsync(query);
            foreach (var list in lists)
            {
                await this.DeleteAsync(list);
            }
        }

    }
}
