namespace allspice.Repositories;

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
    ingredients (name, quantity,recipeId,creatorId)
    VALUES (@Name,@Quantity,@RecipeId,@CreatorId);
    
    SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<Ingredient>(sql, ingredientData);
  }

  internal Ingredient UpdateIngredient(Ingredient ingredientData)
  {
    string sql = @"UPDATE ingredients SET
      name = @Name,
      quantity = @Quantity
      WHERE id = @Id;
      
      SELECT * FROM ingredients WHERE id = @Id;";
    return db.QueryFirstOrDefault<Ingredient>(sql, ingredientData);
  }

  internal void RemoveIngredient(int ingredientId)
  { db.Query("DELETE FROM ingredients WHERE id = @ingredientId", new { ingredientId }); }

}