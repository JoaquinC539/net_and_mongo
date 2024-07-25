namespace BookStoreApi.Bases;
using BookStoreApi.Interfaces;
using Microsoft.AspNetCore.Mvc;


public class BaseApiControllerModel<TService,TModel> :BaseApiController<TModel,TService>,IBaseApiController<TModel> 
where TService:IApiService<TModel>
 where TModel:IEntity
{
    

    public BaseApiControllerModel(TService service):base(service)
    {
        
    }
    
    [HttpPost]
    public virtual async Task<ActionResult<TModel>> Create([FromBody] TModel model)
    {
        var entity=await service.CreateAsync(model);
        if(entity==null)
        {
            return BadRequest();
        }
        return Ok(entity);
    }
    
    [HttpPut("{id:length(24)}")]
    public virtual async Task<ActionResult<TModel>> Update([FromBody] TModel model, [FromRoute] string id)
    {
        var modelUpdate=await service.UpdateAsync(model,id);
        if(modelUpdate==null)
        {
            return NotFound();
        }else
        {
            return Ok(modelUpdate);
        }
    }
    


}