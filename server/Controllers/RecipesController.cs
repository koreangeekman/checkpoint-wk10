namespace wk10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(RecipeService recipeService) : ControllerBase
{
  [HttpGet]
  public ActionResult<List<Recipe>> GetRecipes()
  {
    try { return Ok(recipeService.GetRecipes()); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<List<Recipe>> GetRecipeById(int recipeId)
  {
    try { return Ok(recipeService.GetRecipeById(recipeId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpPost]
  public ActionResult<List<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try { return Ok(recipeService.CreateRecipe(recipeData)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpDelete("{recipeId}")]
  public ActionResult<List<Recipe>> DeleteRecipe(int recipeId)
  {
    try { return Ok(recipeService.DeleteRecipe(recipeId)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [HttpPut("{recipeId}")]
  public ActionResult<List<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
  {
    try { return Ok(recipeService.UpdateRecipe(recipeId, recipeData)); }
    catch (Exception e) { return BadRequest(e.Message); }
  }



}