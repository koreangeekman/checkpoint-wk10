namespace wk10.Repositories;

public class RecipeRepo(IDbConnection db)
{
  internal List<Recipe> GetRecipes()
  { return db.Query<Recipe>("SELECT * FROM recipes;").ToList(); }

  internal Recipe GetRecipeById(int recipeId)
  { return db.QueryFirstOrDefault("SELECT * FROM recipes WHERE id = @recipeId;", new { recipeId }); }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"INSERT INTO 
    recipes (title,instructions,img,category,creatorId)
    VALUES (@Title,@Instructions,@Img,@Category,@CreatorId);

    SELECT * FROM recipes WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault(sql, recipeData);
  }

  internal void DeleteRecipe(int recipeId)
  { db.Query("DELETE FROM recipes WHERE id = @recipeId", new { recipeId }); }

  internal Recipe UpdateRecipe(Recipe recipeData)
  {
    string sql = @"UPDATE recipes SET
      title = @Title,
      instructions = @Instructions,
      category = @Category,
      img = @Img
      WHERE id = @Id;";
    return db.QueryFirstOrDefault(sql, recipeData);
  }

}