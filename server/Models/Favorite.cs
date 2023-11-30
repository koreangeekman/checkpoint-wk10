namespace wk10.Models;

public class Favorite
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
  public Profile Account { get; set; }
  public Recipe Recipe { get; set; }
}