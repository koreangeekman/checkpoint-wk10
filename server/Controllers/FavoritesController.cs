namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly Auth0Provider _a0;
  private readonly FavService _favService;

  public FavoritesController(Auth0Provider a0, FavService favService)
  {
    _a0 = a0;
    _favService = favService;
  }


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<FavRecipe>> CreateFavorite([FromBody] Favorite favData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      favData.AccountId = userInfo.Id;
      return Ok(_favService.CreateFavorite(favData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{favId}")]
  public async Task<ActionResult<Favorite>> DeleteFavorite(int favId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_favService.DeleteFavorite(userInfo.Id, favId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}