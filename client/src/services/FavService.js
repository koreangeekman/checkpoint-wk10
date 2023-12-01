import { AppState } from "../AppState";
import { Recipe } from "../models/Recipe";
import { api } from "./AxiosService";

class FavService {

  async addFavorite(recipeId) {
    const res = api.post('api/favorites', { recipeId })
    AppState.favRecipes.push(new Recipe(res.data));
  }

  async removeFavorite(favId) {
    const res = api.delete('api/favorites/' + favId);
    AppState.favRecipes = AppState.favRecipes.filter(r => r.id != favId);
  }

}

export const favService = new FavService();