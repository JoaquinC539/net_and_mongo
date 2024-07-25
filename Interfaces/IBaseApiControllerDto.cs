using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Interfaces;

public interface IBaseApiControllerDto<TModel,TDto> : IApiController<TModel> where TModel:IEntity
{
   
    Task<ActionResult<TModel>> Create([FromBody] TDto model);
    Task<ActionResult<TModel>> Update([FromBody] TDto model, [FromRoute] string id);
    
}