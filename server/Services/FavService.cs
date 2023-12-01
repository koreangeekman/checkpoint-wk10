namespace wk10.Services;

public class FavService(FavRepo favRepo)
{
  internal List<FavRecipe> GetFavoritesByAccountId(string accountId)
  { return favRepo.GetFavByAccountId(accountId); }

  internal FavRecipe CreateFavorite(Favorite favData)
  { return favRepo.CreateFavorite(favData); }

  internal string DeleteFavorite(string accountId, int favId)
  {
    favRepo.DeleteFavorite(favId);
    return "Favorite association has been deleted";
  }
}