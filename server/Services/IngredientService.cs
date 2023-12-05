namespace wk10.Services;

public class IngredientsService(IngredientsRepo ingredientsRepo, RecipeService recipeService)
{
  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = ingredientsRepo.GetIngredientById(ingredientId);
    if (ingredient == null) { throw new Exception($"No ingredient by ID: {ingredientId}"); }
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  { return ingredientsRepo.GetIngredientsByRecipeId(recipeId); }

  internal Ingredient AddIngredient(string creatorId, Ingredient ingredientData)
  {
    Recipe recipe = recipeService.GetRecipeById(ingredientData.RecipeId);
    if (recipe.CreatorId != creatorId) { throw new Exception("Forbidden action - Not your recipe to modify"); }
    ingredientData.CreatorId = creatorId;
    return ingredientsRepo.AddIngredient(ingredientData);
  }

  internal Ingredient UpdateIngredient(string creatorId, Ingredient ingredientData)
  {
    Ingredient ingredient = GetIngredientById(ingredientData.Id);
    if (ingredient.CreatorId != creatorId) { throw new Exception("Forbidden action - Not your ingredient to modify"); }
    return ingredientsRepo.UpdateIngredient(ingredientData);
  }

  internal string RemoveIngredient(string creatorId, int ingredientId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    if (ingredient.CreatorId != creatorId) { throw new Exception("Forbidden action - Not your ingredient to remove"); }
    ingredientsRepo.RemoveIngredient(ingredientId);
    return "Ingredient removed from recipe";
  }
}