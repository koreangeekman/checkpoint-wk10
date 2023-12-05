namespace wk10.Services;

public class InstructionStepService
{
  private readonly InstructionStepRepo ISRepo;
  private readonly RecipeRepo recipeRepo;

  public InstructionStepService(InstructionStepRepo isRepo, RecipeRepo _recipeRepo)
  {
    ISRepo = isRepo;
    recipeRepo = _recipeRepo;
  }

  internal List<InstructionStep> GetInstructions(int recipeId)
  { return ISRepo.GetInstructions(recipeId); }

  internal InstructionStep GetStepById(int stepId)
  {
    return ISRepo.GetStepById(stepId) ?? throw new Exception("Cannot find by ID");
  }

  // internal int GetStepCountForRecipe(int recipeId)
  // { return ISRepo.GetStepCountForRecipe(recipeId); }

  internal InstructionStep CreateStep(InstructionStep stepData)
  {
    // int steps = GetStepCountForRecipe(stepData.RecipeId);
    // if (stepData.Position == null || stepData.Position > steps + 1 || stepData.Position < 1)
    // { stepData.Position = steps + 1; }
    return ISRepo.CreateStep(stepData);
  }

  internal InstructionStep UpdateStep(string creatorId, InstructionStep stepData)
  {
    InstructionStep step = CheckOwner(creatorId, stepData.Id);
    step.Position = stepData.Position ?? step.Position;
    step.Body = stepData.Body ?? step.Body;
    return ISRepo.UpdateStep(stepData);
  }

  internal string RemoveStep(string creatorId, int stepId)
  {
    InstructionStep step = CheckOwner(creatorId, stepId);
    ISRepo.RemoveStep(stepId);
    return $"Step {step.Position} removed";
  }

  private InstructionStep CheckOwner(string creatorId, int stepId)
  {
    InstructionStep step = GetStepById(stepId);
    Recipe recipe = recipeRepo.GetRecipeById(step.RecipeId);
    if (recipe.CreatorId != creatorId) { throw new Exception("Forbidden action"); }
    return step;
  }
}