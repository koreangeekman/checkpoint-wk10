<template>
  <div class="recipe-card rounded shadow p-2 selectable position-relative" @click="selectRecipe(recipe)">
    <div class="d-flex flex-column justify-content-between h-100">
      <p class="mb-0 category shadow rounded-pill bg-grey fw-bold px-3 py-1">{{ recipe.category }}</p>
      <span class="shadow rounded bg-grey text-start px-2 py-1">
        <p class="mb-0 fw-bold"> {{ recipe.title }}</p>
        <p class="mb-0"> {{ recipe.instructions }}</p>
      </span>
    </div>
    <div class="position-absolute favs" v-if="account.id">
      <Favorites :recipeId="recipe.id" />
    </div>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { computed } from 'vue';
import { Modal } from "bootstrap";
import { AppState } from '../AppState';
import { Recipe } from "../models/Recipe";
import Favorites from "./Favorites.vue";
import { recipeService } from "../services/RecipeService";

export default {
  props: { recipe: { type: Recipe } },
  setup(props) {
    async function _getRecipeById(recipeId) {
      try {
        await recipeService.getRecipeById(recipeId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function _getIngredientsByRecipeId(recipeId) {
      try {
        await recipeService.getIngredientsByRecipeId(recipeId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      imgUrl: computed(() => `url('${props.recipe.img}')`),
      account: computed(() => AppState.account),
      async selectRecipe(recipeObj) {
        AppState.activeRecipe = recipeObj; // for user experience, immediately populate
        _getRecipeById(recipeObj.id); // get in case of any changes since page load
        _getIngredientsByRecipeId(recipeObj.id); // not currently set to populate on recipe
        Modal.getOrCreateInstance('#recipeDetails').show();
      },
    };
  },
  components: { Favorites }
};
</script>


<style lang="scss" scoped>
.recipe-card {
  background-image: v-bind(imgUrl);
  background-position: center;
  background-size: cover;
  height: 15rem;
  width: 100%;
  transition: .25s;

  .category {
    width: fit-content;
  }

  .bg-grey {
    background-color: #80808080;
    color: white;
    text-shadow: 0 0 3px black;
  }

  .favs {
    top: 0;
    right: .5rem;
  }
}

.recipe-card:hover {
  transform: scale(103%);
}
</style>