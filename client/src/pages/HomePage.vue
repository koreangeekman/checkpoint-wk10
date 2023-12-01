<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12 d-flex justify-content-center position-relative">
        <span class="d-flex justify-content-between position-absolute filter shadow rounded px-2 py-1">
          <button class="mx-2 btn selectable" @click="sortBy('all')">Show All</button>
          <button class="mx-2 btn selectable" @click="sortBy('mine')">My Recipes</button>
          <button class="mx-2 btn selectable" @click="sortBy('favs')">Favorites</button>
        </span>
      </div>
      <div class="col-12 col-md-4 text-center p-4" v-for="recipe in recipes">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
  </div>
  <ModalComponent :modalId="'newRecipe'" :modalSize="'modal-lg'" :showHeader="true">
    <template #modalTitle>New Recipe</template>
    <template #modalBody>
      <RecipeForm />
    </template>
    <!-- <template #modalFooter>submit button</template> -->
  </ModalComponent>
  <ModalComponent :modalId="'editRecipe'" :modalSize="'modal-lg'" :showHeader="true">
    <template #modalTitle>Edit Recipe</template>
    <template #modalBody>
      <RecipeForm :edit="true" />
    </template>
    <!-- <template #modalFooter>submit button</template> -->
  </ModalComponent>
  <ModalComponent :modalId="'recipeDetails'" :modalSize="'modal-xl'">
    <!-- <template #modalTitle></template> -->
    <template #modalBody>
      boop
    </template>
    <!-- <template #modalFooter>submit button</template> -->
  </ModalComponent>
</template>

<script>
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { computed, onMounted, ref, watchEffect } from "vue";
import RecipeCard from "../components/RecipeCard.vue";
import RecipeForm from "../components/RecipeForm.vue";
import ModalComponent from "../components/ModalComponent.vue";
import { recipeService } from "../services/RecipeService";

export default {
  setup() {

    const filterBy = ref('');
    const filtered = ref([]);
    // watchEffect(() => {
    //   if (filterBy = 'favs') {
    //     // filtered.value = AppState.recipes.filter()
    //   }
    // })

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
.filter {
  top: -2.5rem;
  background-color: #ffffffb9;
}

.text-grey {
  color: #808080;
  font-size: smaller;
}
</style>
