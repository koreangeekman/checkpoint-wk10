namespace wk10.Services;

public class AccountService(AccountsRepository repo)
{
  internal Account GetProfileByEmail(string email)
  {
    return repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
    return repo.Edit(original);
  }
}
