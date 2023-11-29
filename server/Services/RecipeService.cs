namespace wk10.Services;

public class RecipeService(RecipeRepo recipeRepo)
{
  internal List<Recipe> GetRecipes()
  { return recipeRepo.GetRecipes(); }

  internal Recipe GetRecipeById(int recipeId)
  { return recipeRepo.GetRecipeById(recipeId); }

  internal Recipe CreateRecipe(Recipe recipeData)
  { return recipeRepo.CreateRecipe(recipeData); }

  internal string DeleteRecipe(int recipeId)
  {
    recipeRepo.DeleteRecipe(recipeId);
    return "Recipe deleted";
  }

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeData)
  {
    Recipe recipe = GetRecipeById(recipeId);

    recipe.Title = recipeData.Title ?? recipe.Title;
    recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
    recipe.Category = recipeData.Category ?? recipe.Category;
    recipe.Img = recipeData.Img ?? recipe.Img;

    return recipeRepo.UpdateRecipe(recipe);
  }


}