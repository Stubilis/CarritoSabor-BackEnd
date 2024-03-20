using FlavorCart.Models;
using FlavorCart.Enums;

using Google.Cloud.Firestore;

namespace FlavorCart.Repositories
{
    public class PriceRepository
    {
        private readonly BaseRepository<Price> _repository;
        public PriceRepository()
        {
            // This should be injected - This is just an example.
            _repository = new BaseRepository<Price>(Collection.Users);
        }

        public async Task<List<Price>> GetAllAsync() => await _repository.GetAllAsync<Price>();

        public async Task<Price?> GetAsync(Price entity) => (Price?)await _repository.GetAsync(entity);

        public async Task<Price> AddAsync(Price entity) => await _repository.AddAsync(entity);

        public async Task<Price> UpdateAsync(Price entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Price entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Price>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Price>(query);

        // This is specific to Price.
        
        public async Task<List<Price>> GetPriceWhereArticle(string articleId)
        {
            var articles = new List<Article>()
        {
            new()
            {
                Id=articleId
            }
        };

            var query = _repository._firestoreDb.Collection(Collection.Prices.ToString()).WhereIn(nameof(Price.ArticleId), articles);
            return await this.QueryRecordsAsync(query);
        }
        
    }
}
