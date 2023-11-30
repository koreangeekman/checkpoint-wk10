namespace wk10.Repositories;

public class RecipeRepo(IDbConnection db)
{

  private Recipe PopulateProfile(Recipe recipe, Profile profile)
  {
    recipe.Creator = profile;
    return recipe;
  }

  internal List<Recipe> GetRecipes()
  {
    string sql = @"
        SELECT 
        r.*,
        acc.*
        FROM recipes r
        JOIN accounts acc ON acc.id = r.creatorId;";
    return db.Query<Recipe, Profile, Recipe>(sql, PopulateProfile).ToList();
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
        SELECT 
        r.*,
        acc.*
        FROM recipes r
        JOIN accounts acc ON acc.id = r.creatorId
        WHERE r.id = @recipeId;";
    return db.Query<Recipe, Profile, Recipe>(sql, PopulateProfile, new { recipeId }).FirstOrDefault();
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"INSERT INTO 
    recipes (title,instructions,img,category,creatorId)
    VALUES (@Title,@Instructions,@Img,@Category,@CreatorId);

    SELECT * FROM recipes WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<Recipe>(sql, recipeData);
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
    return db.QueryFirstOrDefault<Recipe>(sql, recipeData);
  }

}