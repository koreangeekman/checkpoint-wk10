namespace wk10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController(Auth0Provider auth0Provider, FavService favService) : ControllerBase
{

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favData)
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favData.AccountId = userInfo.Id;
      return Ok(favService.CreateFavorite(favData));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpDelete("{favId}")]
  public async Task<ActionResult<Favorite>> DeleteFavorite(int favId)
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(favService.DeleteFavorite(userInfo.Id, favId));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}