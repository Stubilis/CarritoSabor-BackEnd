using FlavorCart.Models;
using FlavorCart.Enums;

using Google.Cloud.Firestore;


namespace FlavorCart.Repositories
{
    public class PriceRepository
    {
        private readonly BaseRepository<Price> _repository;
        private readonly BaseRepository<Article> _artRepository;
        public PriceRepository()
        {
            // This should be injected - This is just an example.
            _repository = new BaseRepository<Price>(Collection.Prices);
            _artRepository = new BaseRepository<Article>(Collection.Articles);
        }

        public async Task<List<Price>> GetAllAsync() => await _repository.GetAllAsync<Price>();

        public async Task<Price?> GetAsync(Price entity) => (Price?)await _repository.GetAsync(entity);
        public async Task<Price> AddAsync(Price entity)
        {
            await _repository.AddAsync(entity);
            //When a price is added, the average price of the article is updated
            await UpdateAvgPriceAsync(entity);
            return entity;
        }



        public async Task<Price> UpdateAsync(Price entity)
        {
            await UpdateAvgPriceAsync(entity);
            return await _repository.UpdateAsync(entity);
        }

        //Update the average price of the article
        public async Task<Article> UpdateAvgPriceAsync(Price entity)
        {
            Article article = new Article()
            {
                Id = entity.ArticleId
            };

            article = (Article)await _artRepository.GetAsync(article);
            float medium = article.AveragePrice;
            if (medium == 0)
            {
                article.AveragePrice = entity.Cost;
            }
            else
            {
                article.AveragePrice = await calcAvgPriceAsync(article.Id);

            }
            return await _artRepository.UpdateAsync(article);

        }
        public async Task DeleteAsync(Price entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Price>> QueryRecordsAsync(Google.Cloud.Firestore.Query query) => await _repository.QueryRecordsAsync<Price>(query);

        // This is specific to Price.

        public async Task<List<Price>> GetPriceByArticle(string articleId)
        {

            var query = _repository._firestoreDb.Collection("Prices").WhereEqualTo("ArticleId", articleId);
            return await this.QueryRecordsAsync(query);
        }
        public async Task<float> calcAvgPriceAsync(string articleId)
        {
            var query = _repository._firestoreDb.Collection("Prices").WhereEqualTo("ArticleId", articleId);
            var prices = await this.QueryRecordsAsync(query);
            float sum = 0;
            foreach (Price price in prices)
            {
                sum += price.Cost;
            }
            Console.WriteLine("Sum: " + sum + "/" + prices.Count);
            float avg = sum / prices.Count;
            return avg;
        }
        //borrar los precios de la lista asociados a idArticulo
        public async Task DeletePricesByArticle(string articleId)
        {
            var query = _repository._firestoreDb.Collection("Prices").WhereEqualTo("ArticleId", articleId);
            var prices = await this.QueryRecordsAsync(query);
            foreach (Price price in prices)
            {
                await this.DeleteAsync(price);
            }
        }
        //borrar los precios de la lista asociados a idArticulo
    }


}
