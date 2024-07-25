using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Interfaces;

public interface IBaseApiController<TModel> : IApiController<TModel> where TModel:IEntity
{
    
    Task<ActionResult<TModel>> Create([FromBody] TModel model);
    Task<ActionResult<TModel>> Update([FromBody] TModel model, [FromRoute] string id);
    
}