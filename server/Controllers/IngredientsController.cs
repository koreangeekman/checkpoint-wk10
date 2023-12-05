namespace wk10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{

  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _a0;

  public IngredientsController(Auth0Provider a0, IngredientsService ingredientsService)
  {
    _a0 = a0;
    _ingredientsService = ingredientsService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Ingredient>> AddIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_ingredientsService.AddIngredient(userInfo.Id, ingredientData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{ingredientId}")]
  public async Task<ActionResult<Ingredient>> RemoveIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_ingredientsService.RemoveIngredient(userInfo.Id, ingredientId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}