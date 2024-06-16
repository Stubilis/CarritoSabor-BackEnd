using FlavorCart.Models;
using FlavorCart.Enums;

using Google.Cloud.Firestore;

namespace FlavorCart.Repositories
{
    public class CategoryRepository
    {
        private readonly BaseRepository<Category> _repository;
        public CategoryRepository()
        {
            _repository = new BaseRepository<Category>(Collection.Categories);
        }

        public async Task<List<Category>> GetAllAsync() => await _repository.GetAllAsync<Category>();

        public async Task<Category?> GetAsync(Category entity) => (Category?)await _repository.GetAsync(entity);

        public async Task<Category> AddAsync(Category entity) => await _repository.AddAsync(entity);

        public async Task<Category> UpdateAsync(Category entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Category entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Category>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Category>(query);

        
    }
}
