namespace wk10.Repositories;

public class IngredientsRepo(IDbConnection db)
{
  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = "SELECT * FROM ingredients WHERE id = @ingredientId;";
    return db.QueryFirstOrDefault<Ingredient>(sql, new { ingredientId });
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = "SELECT * FROM ingredients WHERE recipeId = @RecipeId;";
    return db.Query<Ingredient>(sql, new { recipeId }).ToList();
  }

  internal Ingredient AddIngredient(Ingredient ingredientData)
  {
    string sql = @"INSERT INTO 
    ingredients (name, quantity,recipeId)
    VALUES (@Name,@Quantity,@RecipeId);
    
    SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<Ingredient>(sql, ingredientData);
  }

  internal void RemoveIngredient(int ingredientId)
  { db.Query("DELETE FROM ingredients WHERE id = @ingredientId", new { ingredientId }); }

}