namespace wk10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly RecipeService _recipeService;
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _a0;

  public RecipesController(Auth0Provider a0, RecipeService recipeService, IngredientsService ingredientsService)
  {
    _a0 = a0;
    _recipeService = recipeService;
    _ingredientsService = ingredientsService;
  }


  [HttpGet]
  public ActionResult<List<Recipe>> GetRecipes()
  {
    try { return Ok(_recipeService.GetRecipes()); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try { return Ok(_recipeService.GetRecipeById(recipeId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try { return Ok(_ingredientsService.GetIngredientsByRecipeId(recipeId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      return Ok(_recipeService.CreateRecipe(recipeData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_recipeService.DeleteRecipe(userInfo.Id, recipeId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPut("{recipeId}")]
  public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_recipeService.UpdateRecipe(recipeId, recipeData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }



}