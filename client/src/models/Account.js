export class Account {
  constructor(data) {
    this.id = data.id
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    this.website = data.website
    this.linkedin = data.linkedin
    this.github = data.github
    this.resume = data.resume
    this.bio = data.bio
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
  }
}
