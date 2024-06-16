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

        public async Task<List<UserToken>> GetUserByEmailAsync(UserToken entity)
        {
            var query = _repository._firestoreDb.Collection("UserTokens").WhereEqualTo("Email", entity.Email);
            return await this.QueryRecordsAsync(query);
        }
        public async Task<UserToken> UpdateAsyncByEmail(UserToken entity)
        {
            var query = _repository._firestoreDb.Collection("UserTokens").WhereEqualTo("Email", entity.Email);
            var user = await this.QueryRecordsAsync(query);
            //
            return await _repository.UpdateAsync(user[0]);
        }

        internal async Task UpdateAsync(List<UserToken> result)
        {
            foreach (var user in result)
            {
                await _repository.UpdateAsync(user);
            }
        }
    }
}
