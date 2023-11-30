namespace wk10.Services;

public class IngredientsService(IngredientsRepo ingredientsRepo)
{
  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = ingredientsRepo.GetIngredientById(ingredientId);
    if (ingredient == null) { throw new Exception($"No ingredient by ID: {ingredientId}"); }
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  { return ingredientsRepo.GetIngredientsByRecipeId(recipeId); }

  internal Ingredient AddIngredient(Ingredient ingredientData)
  { return ingredientsRepo.AddIngredient(ingredientData); }

  internal string RemoveIngredient(string creatorId, int ingredientId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    if (ingredient.CreatorId != creatorId) { throw new Exception("Forbidden action - Not your data"); }
    ingredientsRepo.RemoveIngredient(ingredientId);
    return "Ingredient removed from recipe";
  }
}