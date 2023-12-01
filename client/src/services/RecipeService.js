import { AppState } from "../AppState";
import { Ingredient } from "../models/Ingredient";
import { Recipe } from "../models/Recipe";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

function recipeIndex(recipeId) {
  return AppState.recipes.findIndex(r => r.id = recipeId);
}
class RecipeService {

  async getRecipes() {
    const res = await api.get('api/recipes');
    AppState.recipes = res.data.map(r => new Recipe(r));
  }

  async getRecipeById(recipeId) {
    const res = await api.get('api/recipes/' + recipeId);
    const recipe = new Recipe(res.data);
    AppState.activeRecipe = recipe;
    const index = recipeIndex(recipeId)
    AppState.recipes.splice(index, 1, recipe) // splice in new copy in case of changes since page load
  }
  async getIngredientsByRecipeId(recipeId) {
    const res = await api.get('api/recipes/' + recipeId + '/ingredients');
    AppState.ingredients = res.data.map(i => new Ingredient(i));
  }

  async createRecipe(recipeData) {
    const res = await api.post('api/recipes', recipeData);
    AppState.recipes.push(new Recipe(res.data));
  }

  async deleteRecipe(recipeId) {
    const res = await api.delete('api/recipes/' + recipeId);
    AppState.recipes = AppState.recipes.filter(r => r.id != recipeId);
    logger.log(`Recipe with ID ${recipeId} has been deleted`)
  }

  async updateRecipe(recipeData) {
    const res = await api.put('api/recipes/' + recipeData.id, recipeData);
    const index = recipeIndex(recipeData.id)
    AppState.recipes.splice(index, 1, recipeData);
  }
}

export const recipeService = new RecipeService();