using BookStoreApi.Models;

namespace BookStoreApi.Interfaces;

public interface IApiService<TModel>
{
    public Task<ICollection<TModel>> GetAllAsync();
    public Task<TModel?> GetFromIdAsync(string id);
    

    public Task<TModel?> CreateAsync(TModel model);

    public Task<TModel?> CreateAsync(IEntityDto dto);
    public Task<TModel?> UpdateAsync(TModel model,string id);
    public Task<TModel?> UpdateAsync(IEntityDto model,string id);

    public Task DeleteAsync(string id);
}