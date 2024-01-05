namespace wk10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly Auth0Provider a0;
  private readonly RecipeService recipeService;
  private readonly IngredientsService ingredientsService;
  private readonly InstructionStepService iStepService;

  public RecipesController(
    Auth0Provider _a0,
    RecipeService _recipeService,
    IngredientsService _ingredientsService,
    InstructionStepService _iStepService)
  {
    a0 = _a0;
    recipeService = _recipeService;
    ingredientsService = _ingredientsService;
    iStepService = _iStepService;
  }


  [HttpGet]
  public ActionResult<List<Recipe>> GetRecipes(string query)
  {
    try { return Ok(recipeService.GetRecipes(query)); }
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

  [HttpGet("{recipeId}/instructions")]
  public ActionResult<List<InstructionStep>> GetInstructions(int recipeId)
  {
    try { return Ok(iStepService.GetInstructions(recipeId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
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
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
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
      Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(recipeService.UpdateRecipe(recipeId, recipeData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }



}