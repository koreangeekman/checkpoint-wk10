<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12 d-flex justify-content-center position-relative" v-if="account?.id">
        <span class="d-flex justify-content-between position-absolute filter shadow rounded px-2 py-1">
          <button class="mx-2 btn selectable" @click="applyFilter('all')">Show All</button>
          <button class="mx-2 btn selectable" @click="applyFilter('mine')">My Recipes</button>
          <button class="mx-2 btn selectable" @click="applyFilter('favs')">Favorites</button>
        </span>
      </div>
      <div class="col-12 col-md-6 col-lg-4 text-center p-4" v-for="recipe in recipes">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
  </div>

  <ModalComponent :modalId="'newRecipe'" :modalSize="'modal-lg'" :showHeader="true">
    <template #modalTitle>New Recipe</template>
    <template #modalBody>
      <RecipeForm />
    </template>
  </ModalComponent>

  <ModalComponent :modalId="'editRecipe'" :modalSize="'modal-lg'" :showHeader="true">
    <template #modalTitle>Edit Recipe</template>
    <template #modalBody>
      <RecipeForm :edit="true" />
    </template>
  </ModalComponent>

  <ModalComponent :modalId="'recipeDetails'" :modalSize="'modal-xl'">
    <!-- <template #modalTitle></template> -->
    <template #modalBody>
      <RecipeDetails />
    </template>
  </ModalComponent>
</template>

<script>
import Pop from "../utils/Pop";
import { AppState } from "../AppState";
import { computed, onMounted, ref, watchEffect } from "vue";
import { accountService } from "../services/AccountService";
import { recipeService } from "../services/RecipeService";
import ModalComponent from "../components/ModalComponent.vue";
import RecipeDetails from "../components/RecipeDetails.vue";
import RecipeCard from "../components/RecipeCard.vue";
import RecipeForm from "../components/RecipeForm.vue";

export default {
  setup() {

    const checked = ref(false);
    const filterBy = ref('all');
    const recipes = ref([...AppState.recipes]);

    watchEffect(() => {
      if (!checked && AppState.account.id && AppState.favRecipes.length == 0) {
        _getFavRecipes();
      }
      if (AppState.queried.length > 0) {
        recipes.value = AppState.queried;
      } else if (filterBy.value || AppState.recipes.length > 0) {
        _filtering()
      }
    })

    function _filtering() {
      if (filterBy.value == 'favs') {
        recipes.value = AppState.favRecipes;
      } else if (filterBy.value == 'mine') {
        recipes.value = [...AppState.recipes].filter(r => r.creatorId == AppState.account.id);
      } else {
        recipes.value = AppState.recipes;
      }
    }

    onMounted(() => {
      _getRecipes();
    })

    async function _getRecipes() {
      try {
        await recipeService.getRecipes();
        recipes.value = [...AppState.recipes];
      }
      catch (error) { Pop.error(error); }
    }

    async function _getFavRecipes() {
      try {
        await accountService.getFavsByAccountId();
        checked = true;
      }
      catch (error) { Pop.error(error); }
    }

    return {
      filterBy,
      recipes,
      account: computed(() => AppState.account),

      applyFilter(filter) {
        filterBy.value = filter;
        _filtering();
      }

    };
  },
  components: { RecipeCard, RecipeForm, ModalComponent, RecipeDetails }
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
