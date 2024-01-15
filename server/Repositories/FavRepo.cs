namespace allspice.Repositories;

public class FavRepo(IDbConnection db)
{
  private FavRecipe ReformatWithFavId(Favorite fav, FavRecipe favR)
  {
    favR.FavoriteId = fav.Id;
    return favR;
  }

  internal List<FavRecipe> GetFavByAccountId(string accountId)
  {
    string sql = @"
      SELECT 
      fav.*,
      r.* 
      FROM favorites fav 
      JOIN recipes r ON fav.recipeId = r.id
      WHERE fav.accountId = @AccountId;";
    return db.Query<Favorite, FavRecipe, FavRecipe>(sql, ReformatWithFavId, new { accountId }).ToList();
  }

  internal FavRecipe CreateFavorite(Favorite favData)
  {
    string sql = @"INSERT INTO 
      favorites(recipeId,accountId)
      VALUES (@RecipeId,@AccountId);
      
      SELECT 
      fav.*,
      r.* 
      FROM favorites fav 
      JOIN recipes r ON fav.recipeId = r.id
      WHERE fav.id = LAST_INSERT_ID();";
    return db.Query<Favorite, FavRecipe, FavRecipe>(sql, ReformatWithFavId, favData).FirstOrDefault();
  }

  internal void DeleteFavorite(int favId)
  { db.Query("DELETE FROM favorites WHERE id = @favId", new { favId }); }
}