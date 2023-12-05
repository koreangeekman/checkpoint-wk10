export class InstructionStep {
  constructor(data) {
    this.id = data.id
    this.position = data.position
    this.body = data.body
    this.recipeId = data.recipeId
    this.edit = false
  }
}