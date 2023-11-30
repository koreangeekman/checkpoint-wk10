export class Favorite {
  constructor(data) {
    this.id = data.id
    this.recipeId = data.recipeId
    this.recipe = data.recipe
    this.accountId = data.accountId
    this.account = data.account
  }
}