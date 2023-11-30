<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12 col-md-4 text-center p-4" v-for="recipe in recipes">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
  </div>
  <ModalComponent :modalId="'newRecipe'">
    <template #modalTitle>New Recipe</template>
    <template #modalBody>
      <form @submit.prevent="createRecipe()" class="">
        <div class="mb-3">
          <label for="title">Title</label>
          <input v-model="recipeForm.title" type="text" class="form-control" name="title" maxlength="50" required
            placeholder="Recipe name...">
        </div>
        <div class="mb-3">
          <label for="instructions">Instructions</label>
          <textarea v-model="recipeForm.instructions" class="form-textarea" name="instructions" maxlength="1000" required
            placeholder="Instructions..."></textarea>
        </div>
      </form>
    </template>
    <!-- <template #modalFooter>submit button</template> -->
  </ModalComponent>
</template>

<script>
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { computed, onMounted, ref } from "vue";
import RecipeCard from "../components/RecipeCard.vue";
import ModalComponent from "../components/ModalComponent.vue";
import { recipeService } from "../services/RecipeService";

export default {
  setup() {

    const recipeForm = ref({});

    async function _getRecipes() {
      try { recipeService.getRecipes(); }
      catch (error) { Pop.error(error); }
    }

    onMounted(() => {
      _getRecipes();
    })

    return {
      recipeForm,

      recipes: computed(() => AppState.recipes),

      async createRecipe() {
        try {
          await recipeService.createRecipe(recipeForm.value);
          Modal.getOrCreateInstance('#newRecipe').hide();
          recipeForm = {};
        }
        catch (error) { Pop.error(error); }
      },

    };
  },
  components: { RecipeCard, ModalComponent }
}
</script>

<style scoped lang="scss"></style>
