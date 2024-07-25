using BookStoreApi.Interfaces;
using MongoDB.Driver;
namespace BookStoreApi.Bases;
public abstract class BaseApiService<TModel> : IApiService<TModel> where TModel:IEntity
{
    private string Collection;
    protected readonly IMongoCollection<TModel> mainCollection;


    public BaseApiService(IMongoDatabase database,string collection)
    {
        this.Collection=collection;
        this.mainCollection=database.GetCollection<TModel>(Collection);
    }
    
    public virtual async Task<TModel?> CreateAsync(TModel model)
    {
        await mainCollection.InsertOneAsync(model);
        return model;
    }
    public virtual async Task DeleteAsync(string id)
    {
        await mainCollection.DeleteOneAsync(e=>e.Id==id);
        
        
    }

    public virtual async Task<ICollection<TModel>> GetAllAsync()
    {
        var entities=await mainCollection.Find(_=>true).ToListAsync();
        return entities;
    }

    public virtual async Task<TModel?> GetFromIdAsync(string id)
    {
        var entity=await mainCollection.Find(e=>e.Id==id).FirstOrDefaultAsync();
        return entity==null ? default : entity;
        
    }

    public virtual async Task<TModel?> UpdateAsync(TModel model, string id)
    {
        var entity =await mainCollection.Find(e=>e.Id==id).FirstOrDefaultAsync();
        if(entity==null)
        {
            return default;
        }
        model.Id= entity.Id;
        FilterDefinition<TModel> filter=Builders<TModel>.Filter.Eq(e=>e.Id,id);
        var entityReplace=await mainCollection.ReplaceOneAsync(filter,model);
        if(entityReplace.IsAcknowledged && entityReplace.MatchedCount>0)
        {
            return model;
        }
        return default;

    }

    public virtual Task<TModel?> CreateAsync(IEntityDto dto)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TModel?> UpdateAsync(IEntityDto model, string id)
    {
        throw new NotImplementedException();
    }
}