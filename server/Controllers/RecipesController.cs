namespace wk10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(
    RecipeService recipeService,
    IngredientsService ingredientsService,
    Auth0Provider auth0Provider
  ) : ControllerBase
{
  [HttpGet]
  public ActionResult<List<Recipe>> GetRecipes()
  {
    try { return Ok(recipeService.GetRecipes()); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try { return Ok(recipeService.GetRecipeById(recipeId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try { return Ok(ingredientsService.GetIngredientsByRecipeId(recipeId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      return Ok(recipeService.CreateRecipe(recipeData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(recipeService.DeleteRecipe(userInfo.Id, recipeId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPut("{recipeId}")]
  public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(recipeService.UpdateRecipe(recipeId, recipeData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }



}