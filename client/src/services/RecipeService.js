import { AppState } from "../AppState";
import { Ingredient } from "../models/Ingredient";
import { Recipe } from "../models/Recipe";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class RecipeService {

  async getRecipes() {
    const res = await api.get('api/recipes');
    AppState.recipes = res.data.map(r => new Recipe(r));
  }

  async getRecipeById(recipeId) {
    const res = await api.get('api/recipes/' + recipeId);
    AppState.activeRecipe = new Recipe(res.data);
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

  }
}

export const recipeService = new RecipeService();