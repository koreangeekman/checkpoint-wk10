export class Recipe {
  constructor(data) {
    this.id = data.id
    this.favoriteId = data.favoriteId
    this.title = data.title
    this.subtitle = data.subtitle
    this.instructions = data.instructions
    this.category = data.category
    this.img = data.img
    this.creatorId = data.creatorId
    this.creator = data.creator
    this.ingredients = data.ingredients
  }
}