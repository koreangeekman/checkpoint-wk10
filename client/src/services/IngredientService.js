import { AppState } from "../AppState";
import { Ingredient } from "../models/Ingredient";
import { api } from "./AxiosService";

class IngredientService {

  async addIngredient(ingredientData) {
    AppState.ingredients.push(new Ingredient(ingredientData)); // for UX, immediate feed in - replace with proper info after
    const res = await api.post('api/ingredients', ingredientData);
    AppState.ingredients.splice(AppState.ingredients.length - 1, 1, new Ingredient(res.data));
    //can rebound-remove on error in 'controller' catch as insurance
  }

  async updateIngredient(ingredientData) {
    const res = await api.delete('api/ingredients/' + ingredientData.id, ingredientData);
    AppState.ingredients = AppState.ingredients.filter(i => i.id != ingredientData.id);
  }

  async removeIngredient(ingredientId) {
    const res = await api.delete('api/ingredients/' + ingredientId);
    AppState.ingredients = AppState.ingredients.filter(i => i.id != ingredientId);
  }

}

export const ingredientService = new IngredientService();