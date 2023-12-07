<template>
  <form @submit.prevent="routeSubmit()" class="">
    <span class="d-flex">
      <div class="me-2 mb-3 w-100">
        <label for="title">Title</label>
        <input v-model="recipeForm.title" type="text" class="form-control shadow" id="title" maxlength="50" required
          placeholder="Recipe name...">
        <span class="d-flex justify-content-between text-grey px-2">
          <p class="mb-0"></p>
          <p class="mb-0 tiny">{{ (recipeForm.title?.length ?? 0) + '/50' }}</p>
        </span>
      </div>
      <div class="ms-2 mb-3 w-50">
        <label for="category">Category</label>
        <select v-model="recipeForm.category" type="text" class="form-select shadow" id="category" required>
          <option v-for="category in categories" :value="category">{{ category }}</option>
        </select>
      </div>
    </span>
    <span class="d-flex">
      <div class="me-2 mb-3 w-100">
        <label for="subtitle">Sub-Title</label>
        <input v-model="recipeForm.subtitle" type="text" class="form-control shadow" id="subtitle" maxlength="50" required
          placeholder="Summarize...">
        <span class="d-flex justify-content-between text-grey px-2">
          <p class="mb-0 tiny">A brief description of the recipe</p>
          <p class="mb-0 tiny">{{ (recipeForm.subtitle?.length ?? 0) + '/50' }}</p>
        </span>
      </div>
      <div class="ms-2 mb-3 w-100">
        <label for="img">Cover Image</label>
        <input v-model="recipeForm.img" type="url" class="form-control shadow" id="img" maxlength="360" required
          placeholder="Image URL...">
        <span class="d-flex justify-content-between text-grey px-2">
          <p class="mb-0 tiny"></p>
          <p class="mb-0 tiny">{{ (recipeForm.img?.length ?? 0) + '/360' }}</p>
        </span>
      </div>
    </span>
    <div class="mb-3">
      <label for="instructions">Instructions</label>
      <textarea v-model="recipeForm.instructions" class="form-control shadow" id="instructions" maxlength="2000" rows="6"
        required placeholder="What to do..."></textarea>
      <span class="d-flex justify-content-between text-grey px-2">
        <p class="mb-0 tiny">A detailed outline of the instructions</p>
        <p class="mb-0 tiny">{{ (recipeForm.instructions?.length ?? 0) + '/2000' }}</p>
      </span>
    </div>
    <button v-if="recipeForm.id" class="btn btn-primary shadow" type="submit">Update Recipe</button>
    <button v-else class="btn btn-primary shadow" type="submit">Add Recipe</button>
  </form>
</template>


<script>
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { AppState } from '../AppState';
import { computed, ref, watchEffect } from 'vue';
import { recipeService } from "../services/RecipeService";

export default {
  props: { edit: { type: Boolean, default: false } },

  setup(props) {

    const recipeForm = ref({});

    watchEffect(() => {
      if (props.edit) {
        recipeForm.value = AppState.activeRecipe;
      }
    })

    return {
      recipeForm,

      categories: computed(() => AppState.categories),
      // activeRecipe: computed(() => AppState.activeRecipe),

      routeSubmit() {
        if (recipeForm.value.id) {
          this.updateRecipe();
        } else {
          this.createRecipe();
        }
      },
      async createRecipe() {
        try {
          await recipeService.createRecipe(recipeForm.value);
          Modal.getOrCreateInstance('#newRecipe').hide();
          recipeForm.value = {};
        }
        catch (error) { Pop.error(error); }
      },
      async updateRecipe() {
        try {
          await recipeService.updateRecipe(recipeForm.value);
          Modal.getOrCreateInstance('#editRecipe').hide();
          recipeForm.value = {};
        }
        catch (error) { Pop.error(error); }
      },

    }
  }
};
</script>


<style lang="scss" scoped>
.tiny {
  font-size: .8em;
  color: grey;
}
</style>