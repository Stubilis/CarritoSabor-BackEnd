using FlavorCart.Models;
using FlavorCart.Enums;

using Google.Cloud.Firestore;

namespace FlavorCart.Repositories
{
    public class RecipeRepository
    {
        private readonly BaseRepository<Recipe> _repository;
        public RecipeRepository()
        {
            _repository = new BaseRepository<Recipe>(Collection.Recipes);
        }

        public async Task<List<Recipe>> GetAllAsync() => await _repository.GetAllAsync<Recipe>();

        public async Task<Recipe?> GetAsync(Recipe entity) => (Recipe?)await _repository.GetAsync(entity);

        public async Task<Recipe> AddAsync(Recipe entity) => await _repository.AddAsync(entity);

        public async Task<Recipe> UpdateAsync(Recipe entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Recipe entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Recipe>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Recipe>(query);

        // This is specific to Recipe.
        
        public async Task<List<Recipe>> GetRecipeByUser(string userId)
        {
             var query = _repository._firestoreDb.Collection(Collection.Recipes.ToString())
                .WhereEqualTo("UserId", userId);
             return await this.QueryRecordsAsync(query);
        }
           
        
    }
}
