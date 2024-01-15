namespace allspice.Services;

public class InstructionStepService
{
  private readonly InstructionStepRepo iStepRepo;
  private readonly RecipeRepo recipeRepo;

  public InstructionStepService(InstructionStepRepo _iStepRepo, RecipeRepo _recipeRepo)
  {
    iStepRepo = _iStepRepo;
    recipeRepo = _recipeRepo;
  }

  internal List<InstructionStep> GetInstructions(int recipeId)
  { return iStepRepo.GetInstructions(recipeId); }

  internal InstructionStep GetStepById(int stepId)
  { return iStepRepo.GetStepById(stepId) ?? throw new Exception("Cannot find by ID"); }

  // internal int GetStepCountForRecipe(int recipeId)
  // { return iStepRepo.GetStepCountForRecipe(recipeId); }

  internal InstructionStep CreateStep(string creatorId, InstructionStep stepData)
  {
    Recipe recipe = recipeRepo.GetRecipeById(stepData.RecipeId);
    if (recipe.CreatorId != creatorId) { throw new Exception("Forbidden action"); }
    // int steps = GetStepCountForRecipe(stepData.RecipeId);
    // if (stepData.Position == null || stepData.Position > steps + 1 || stepData.Position < 1)
    // { stepData.Position = steps + 1; }
    return iStepRepo.CreateStep(stepData);
  }

  internal InstructionStep UpdateStep(string creatorId, InstructionStep stepData)
  {
    InstructionStep step = CheckOwner(creatorId, stepData.Id);
    step.Position = stepData.Position ?? step.Position;
    step.Body = stepData.Body ?? step.Body;
    return iStepRepo.UpdateStep(stepData);
  }

  internal string RemoveStep(string creatorId, int stepId)
  {
    InstructionStep step = CheckOwner(creatorId, stepId);
    iStepRepo.RemoveStep(stepId);
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