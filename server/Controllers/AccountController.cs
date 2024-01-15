namespace allspice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

  private readonly Auth0Provider _a0;
  private readonly AccountService _accountService;
  private readonly FavService _favService;

  public AccountController(RecipeService recipeService, AccountService accountService, Auth0Provider a0, FavService favService)
  {
    _a0 = a0;
    _accountService = accountService;
    _favService = favService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("favorites")]
  [Authorize]
  public async Task<ActionResult<FavRecipe>> GetFavoritesByAccountId()
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_favService.GetFavoritesByAccountId(userInfo.Id));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

  [Authorize]
  [HttpPut]
  public async Task<ActionResult<Profile>> EditProfile([FromBody] Account editData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.Edit(editData, userInfo.Email));
    }
    catch (Exception e) { return BadRequest(e.Message); }
  }

}
