namespace allspice.Models;

public class Profile
{
  public string Id { get; set; }
  public DateTime CreatedAt { get; }
  public DateTime UpdatedAt { get; }
  public string Name { get; set; }
  public string Picture { get; set; }
  public string Website { get; set; }
  public string Github { get; set; }
  public string Linkedin { get; set; }
  public string Resume { get; set; }
  public string Bio { get; set; }
}
