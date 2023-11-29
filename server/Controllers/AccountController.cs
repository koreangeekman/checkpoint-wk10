namespace wk10.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(AccountService accountService, Auth0Provider auth0Provider) : ControllerBase
{
  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
