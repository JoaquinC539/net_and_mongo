using BookStoreApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Bases;

public class BaseApiControllerDto<TService,TModel,TDto>:BaseApiController<TModel,TService>,IBaseApiControllerDto<TModel,TDto>
 where TService:IApiService<TModel>
  where TModel:IEntity 
  where TDto :IEntityDto
{
    

    public BaseApiControllerDto(TService service):base(service)
    {
       
    }

    [HttpPost]
    public virtual async Task<ActionResult<TModel>> Create([FromBody] TDto modelDto)
    {
        var entity =await service.CreateAsync(modelDto);
        if (entity == null)
        {
            return BadRequest();
        }
        return Ok(entity);
    }
    

    [HttpPut("{id:length(24)}")]
    public virtual async Task<ActionResult<TModel>> Update([FromBody] TDto modelDto, [FromRoute] string id)
    {
        var entity=await service.UpdateAsync(modelDto,id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity);
    }
}