using BookStoreApi.Bases;
using BookStoreApi.Models;
using BookStoreApi.Services;

namespace BookStoreApi.Controllers;

public class CategoryController:BaseApiControllerModel<CategoryService,Category>
{
    public CategoryController(CategoryService categoryService):base(categoryService)
    {}
}