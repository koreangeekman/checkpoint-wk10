namespace wk10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider) : ControllerBase
{

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Ingredient>> AddIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(ingredientsService.AddIngredient(userInfo.Id, ingredientData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{ingredientId}")]
  public async Task<ActionResult<Ingredient>> RemoveIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(ingredientsService.RemoveIngredient(userInfo.Id, ingredientId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}