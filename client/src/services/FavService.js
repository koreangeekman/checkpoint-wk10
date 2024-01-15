import { AppState } from "../AppState";
import { Recipe } from "../models/Recipe";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class FavService {

  async addFavorite(recipeId) {
    const res = await api.post('api/favorites', { recipeId })
    AppState.favRecipes.push(new Recipe(res.data));
  }

  async removeFavorite(favId) {
    AppState.favRecipes = AppState.favRecipes.filter(r => r.favoriteId != favId);
    const res = await api.delete('api/favorites/' + favId);
  }

}

export const favService = new FavService();