namespace wk10.Services;

public class RecipeService(RecipeRepo recipeRepo)
{
  internal List<Recipe> GetRecipes()
  { return recipeRepo.GetRecipes(); }

  internal Recipe GetRecipeById(int recipeId)
  { return recipeRepo.GetRecipeById(recipeId) ?? throw new Exception($"Recipe with ID {recipeId} does not exist"); }

  internal Recipe CreateRecipe(Recipe recipeData)
  { return recipeRepo.CreateRecipe(recipeData); }

  internal string DeleteRecipe(string creatorId, int recipeId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != creatorId) { throw new Exception("Forbidden action - Not your data"); }
    recipeRepo.DeleteRecipe(recipeId);
    return "Recipe deleted";
  }

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeData)
  {
    Recipe recipe = GetRecipeById(recipeId);

    recipe.Title = recipeData.Title ?? recipe.Title;
    recipe.Subtitle = recipeData.Subtitle ?? recipe.Subtitle;
    recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
    recipe.Category = recipeData.Category ?? recipe.Category;
    recipe.Img = recipeData.Img ?? recipe.Img;

    recipeRepo.UpdateRecipe(recipe);

    return recipe;
  }


}