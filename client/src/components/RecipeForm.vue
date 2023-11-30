<template>
  <form @submit.prevent="createRecipe()" class="">
    <span class="d-flex">
      <div class="me-2 mb-3 w-100">
        <label for="title">Title</label>
        <input v-model="recipeForm.title" type="text" class="form-control" name="title" maxlength="50" required
          placeholder="Recipe name...">
        <span class="d-flex justify-content-between text-grey px-2">
          <p class="mb-0"></p>
          <p class="mb-0">{{ (recipeForm.title?.length ?? 0) + '/50' }}</p>
        </span>
      </div>
      <div class="ms-2 mb-3 w-50">
        <label for="category">Category</label>
        <select v-model="recipeForm.category" type="text" class="form-control" name="category" required>
          <option v-for="category in categories" :value="category">{{ category }}</option>
        </select>
      </div>
    </span>
    <span class="d-flex">
      <div class="me-2 mb-3 w-100">
        <label for="subtitle">Sub-Title</label>
        <input v-model="recipeForm.subtitle" type="text" class="form-control" name="subtitle" maxlength="50" required
          placeholder="Summarize...">
        <span class="d-flex justify-content-between text-grey px-2">
          <p class="mb-0">A brief description of the receipe</p>
          <p class="mb-0">{{ (recipeForm.subtitle?.length ?? 0) + '/50' }}</p>
        </span>
      </div>
      <div class="ms-2 mb-3 w-100">
        <label for="img">Cover Image</label>
        <input v-model="recipeForm.img" type="url" class="form-control" name="img" maxlength="360" required
          placeholder="Image URL...">
        <span class="d-flex justify-content-between text-grey px-2">
          <p class="mb-0"></p>
          <p class="mb-0">{{ (recipeForm.subtitle?.length ?? 0) + '/360' }}</p>
        </span>
      </div>
    </span>
    <div class="mb-3">
      <label for="instructions">Instructions</label>
      <textarea v-model="recipeForm.instructions" class="form-control" name="instructions" maxlength="2000" rows="6"
        required placeholder="What to do..."></textarea>
      <span class="d-flex justify-content-between text-grey px-2">
        <p class="mb-0">A detailed outline of the instructions</p>
        <p class="mb-0">{{ (recipeForm.instructions?.length ?? 0) + '/2000' }}</p>
      </span>
    </div>
    <button class="btn btn-primary" type="submit">Add Recipe</button>
  </form>
</template>


<script>
import { Modal } from "bootstrap";
import { AppState } from '../AppState';
import { computed, ref, watchEffect } from 'vue';

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


<style lang="scss" scoped></style>