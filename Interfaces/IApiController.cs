using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Interfaces;

public interface IApiController<TModel>
{
    Task<ActionResult<List<TModel>>> Index();
    Task<ActionResult<TModel>> GetById(string id);

    Task<IActionResult> Delete([FromRoute] string id);
}