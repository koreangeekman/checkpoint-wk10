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

  internal Profile Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
    original.Website = editData.Website?.Length > 0 ? editData.Website : original.Website;
    original.Github = editData.Github?.Length > 0 ? editData.Github : original.Github;
    original.Linkedin = editData.Linkedin?.Length > 0 ? editData.Linkedin : original.Linkedin;
    original.Resume = editData.Resume?.Length > 0 ? editData.Resume : original.Resume;
    original.Bio = editData.Bio?.Length > 0 ? editData.Bio : original.Bio;
    return repo.Edit(original);
  }
}
