using Google.Cloud.Firestore;

namespace FlavorCart.Interfaces
{
    /// <summary>
    /// Represents a firestore base repository.
    /// </summary>
    public interface IListsRepository<T>
    {
        /// <summary>
        /// Generates a new google sheet with the data from the repository.
        /// </summary> 
        /// <returns>a records of type T</returns>
        Task<T> GenerateGoogleSheet<T>(T entity) where T : IList;
        /// <summary>
        /// Exports a record to a PDF.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>a record of type T</returns>
        Task<T> ExportPDF<T>(T entity) where T : IList;

     
    }
}
