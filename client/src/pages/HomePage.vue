<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12 col-md-4 text-center p-4" v-for="recipe in recipes">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
  </div>
  <ModalComponent :modalId="'newRecipe'" :modalSize="'modal-lg'">
    <template #modalTitle>New Recipe</template>
    <template #modalBody>
      <RecipeForm />
    </template>
    <!-- <template #modalFooter>submit button</template> -->
  </ModalComponent>
  <ModalComponent :modalId="'editRecipe'" :modalSize="'modal-lg'">
    <template #modalTitle>Edit Recipe</template>
    <template #modalBody>
      <RecipeForm :edit="true" />
    </template>
    <!-- <template #modalFooter>submit button</template> -->
  </ModalComponent>
</template>

<script>
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { computed, onMounted } from "vue";
import RecipeCard from "../components/RecipeCard.vue";
import RecipeForm from "../components/RecipeForm.vue";
import ModalComponent from "../components/ModalComponent.vue";
import { recipeService } from "../services/RecipeService";

export default {
  setup() {

    async function _getRecipes() {
      try { recipeService.getRecipes(); }
      catch (error) { Pop.error(error); }
    }

    onMounted(() => {
      _getRecipes();
    })

    return {
      recipes: computed(() => AppState.recipes),

    };
  },
  components: { RecipeCard, RecipeForm, ModalComponent }
}
</script>

<style scoped lang="scss">
.text-grey {
  color: #808080;
  font-size: smaller;
}
</style>
