using FlavorCart.Models;
using FlavorCart.Enums;

using Google.Cloud.Firestore;

namespace FlavorCart.Repositories
{
    public class UserTokenRepository
    {
        private readonly BaseRepository<UserToken> _repository;
        public UserTokenRepository()
        {
            // This should be injected - This is just an example.
            _repository = new BaseRepository<UserToken>(Collection.UserTokens);
        }

        public async Task<List<UserToken>> GetAllAsync()
        {
            return await _repository.GetAllAsync<UserToken>();
        }

        public async Task<UserToken?> GetAsync(UserToken entity) => (UserToken?)await _repository.GetAsync(entity);

        public async Task<UserToken> AddAsync(UserToken entity) => await _repository.AddAsync(entity);

        public async Task<UserToken> UpdateAsync(UserToken entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(UserToken entity) => await _repository.DeleteAsync(entity);

        public async Task<List<UserToken>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<UserToken>(query);

       
    }
}
