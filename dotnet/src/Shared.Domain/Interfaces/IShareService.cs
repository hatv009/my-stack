namespace Shared.Domain.Interfaces
{
    public interface IShareService<TModelResponse, TModelResquest, TSearch>
    {
        //Task<BlobClient> UploadAzure(IFormFile file);
        //IEnumerable<TModelResponse> Filter(IEnumerable<TModelResponse> query, BaseOpts options);
        //Task<List<TModelResponse>> GetListFromStored(string storedName, SearchOptions options);
        //Task<int> CountAsyncFromStored(string storedName, SearchOptions options);
        Task<int> CountAsync(TSearch options);
        Task<int> CountAsync();
        Task<IEnumerable<TModelResponse>> GetAll(TSearch options);

        Task<TModelResponse> GetById(int id);
        Task<TModelResponse> Create(TModelResquest request);
        Task<TModelResponse> Update(int id, TModelResquest request);
        Task DeleteAsync(int id);

    }
}