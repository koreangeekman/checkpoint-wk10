<template>
  <div class="recipe-card rounded shadow p-2 selectable" data-bs-toggle="modal" data-bs-target="#newRecipe">
    <div class="d-flex flex-column justify-content-between h-100">
      <p class="mb-0 category shadow rounded-pill bg-grey fw-bold px-3 py-1">{{ recipe.category }}</p>
      <span class="shadow rounded bg-grey text-start px-2 py-1">
        <p class="mb-0 fw-bold"> {{ recipe.title }}</p>
        <p class="mb-0"> {{ recipe.instructions }}</p>
      </span>
    </div>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { AppState } from '../AppState';
import { computed, onMounted } from 'vue';
import { Recipe } from "../models/Recipe";
import { recipeService } from "../services/RecipeService";
import { Modal } from "bootstrap";

export default {
  props: { recipe: { type: Recipe } },

  setup(props) {
    async function _getRecipeById(recipeId) {
      try {
        await recipeService.getRecipeById(recipeId)
      }
      catch (error) { Pop.error(error); }
    }
    async function _getIngredientsByRecipeId(recipeId) {
      try {
        await recipeService.getIngredientsByRecipeId(recipeId)
      }
      catch (error) { Pop.error(error); }
    }
    return {
      img: computed(() => `url('${props.recipe.img}')`),

      async selectRecipe(recipeObj) {
        AppState.activeRecipe = recipeObj;
        _getRecipeById(recipeObj.id);
        _getIngredientsByRecipeId(recipeObj.id);
        Modal.getOrCreateInstance('#recipeDetails').show();
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.recipe-card {
  background-image: var(img);
  background-position: center;
  background-size: cover;
  height: 15rem;
  width: 100%;

  .category {
    width: fit-content;
  }

  .bg-grey {
    background-color: #80808080;
  }
}
</style>