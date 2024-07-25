using BookStoreApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Bases;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController<TModel,TService>:ControllerBase,IApiController<TModel>
where TService:IApiService<TModel>
 where TModel:IEntity 
{
    protected TService service;
    public BaseApiController(TService service)
    {
        this.service=service;
    }
    [HttpGet]
    public virtual async Task<ActionResult<List<TModel>>> Index()
    {
        ICollection<TModel> entities=await service.GetAllAsync();
        return Ok(entities);
    }
    [HttpGet]
    [Route("{id:length(24)}")]
    public virtual async Task<ActionResult<TModel>> GetById(string id)
    {
        var entity=await service.GetFromIdAsync(id);
        if(entity==null)
        {
            return NotFound();
        }
        return Ok(entity);
    }
    [HttpDelete("{id:length(24)}")]
    public virtual async Task<IActionResult> Delete([FromRoute] string id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}