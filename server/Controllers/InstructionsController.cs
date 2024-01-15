namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructionsController : ControllerBase
{
  private readonly Auth0Provider a0;
  private readonly InstructionStepService iStepService;

  public InstructionsController(InstructionStepService _iStepService, Auth0Provider _a0)
  {
    iStepService = _iStepService;
    a0 = _a0;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<InstructionStep>> CreateStep([FromBody] InstructionStep stepData)
  {
    Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
    return iStepService.CreateStep(userInfo.Id, stepData);
  }

  [Authorize]
  [HttpPut("{stepId}")]
  public async Task<ActionResult<InstructionStep>> UpdateStep(int stepId, [FromBody] InstructionStep stepData)
  {
    Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
    stepData.Id = stepId; // ensure request data consistency
    return iStepService.UpdateStep(userInfo.Id, stepData);
  }

  [Authorize]
  [HttpDelete("{stepId}")]
  public async Task<ActionResult<string>> RemoveStep(int stepId)
  {
    Account userInfo = await a0.GetUserInfoAsync<Account>(HttpContext);
    return iStepService.RemoveStep(userInfo.Id, stepId);
  }

}