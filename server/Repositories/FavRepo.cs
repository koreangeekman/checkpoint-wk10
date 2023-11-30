namespace wk10.Repositories;

public class FavRepo(IDbConnection db)
{
  internal List<Favorite> GetFavByAccountId(string accountId)
  { return db.Query<Favorite>("SELECT * FROM favorites WHERE accountId = @AccountId;", new { accountId }).ToList(); }

  internal Favorite CreateFavorite(Favorite favData)
  {
    string sql = @"INSERT INTO 
      favorites(recipeId,accountId)
      VALUES (@RecipeId,@AccountId);
      
      SELECT * FROM favorites WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<Favorite>(sql, favData);
  }

  internal void DeleteFavorite(int favId)
  { db.Query("DELETE FROM favorites WHERE id = @favId", new { favId }); }
}