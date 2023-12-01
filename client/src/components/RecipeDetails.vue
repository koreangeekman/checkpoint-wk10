<template>
  <div class="container-fluid">
    <section class="row p-3 m-0" v-if="activeRecipe?.id">
      <div class="col-12 d-md-none">
        <span class="d-flex align-items-center justify-content-between">
          <span class="">
            <p class="mb-0 fw-bold fs-4">{{ activeRecipe.title }}</p>
            <p class="mb-0">{{ activeRecipe.subtitle }}</p>
          </span>
          <span class="d-flex" v-if="activeRecipe.creatorId == account?.id">
            <i class="btn selectable py-0 px-2 text-grey fs-3 mdi mdi-pencil" @click="editRecipe()"></i>
            <i class="btn selectable py-0 px-2 text-danger fs-3 mdi mdi-delete-forever" @click="deleteRecipe()"></i>
          </span>
        </span>
      </div>
      <div class="col-12 col-md-4 m-0 px-0 py-1 position-relative">
        <img :src="activeRecipe.img" :alt="activeRecipe.name" class="img-fluid rounded shadow">
        <p class="mb-0 position-absolute rounded-pill category px-2">{{ activeRecipe.category }}</p>
      </div>
      <div class="col-12 col-md-8 px-3 pb-5">
        <section class="row h-100 pb-2">
          <div class="col-12 d-none d-md-inline">
            <span class="d-flex align-items-center justify-content-between">
              <span class="">
                <p class="mb-0 fw-bold fs-4">{{ activeRecipe.title }}</p>
                <p class="mb-0">{{ activeRecipe.subtitle }}</p>
              </span>
              <span class="d-flex" v-if="activeRecipe.creatorId == account?.id">
                <i class="btn selectable py-0 px-2 text-grey fs-3 mdi mdi-pencil" @click="editRecipe()"></i>
                <i class="btn selectable py-0 px-2 text-danger fs-3 mdi mdi-delete-forever" @click="deleteRecipe()"></i>
              </span>
            </span>
          </div>
          <div class="col-12 col-md-6 p-3 h-100">
            <Instructions :instructions="activeRecipe.instructions" />
          </div>
          <div class="col-12 col-md-6 p-3 h-100">
            <IngredientsList :recipeId="activeRecipe.id" :creatorId="activeRecipe.creatorId" />
          </div>
        </section>
      </div>
    </section>
  </div>
</template>


<script>
import { Modal } from "bootstrap";
import { AppState } from '../AppState';
import { computed } from 'vue';
import IngredientsList from "./IngredientsList.vue";
import Instructions from "./Instructions.vue";
import Pop from "../utils/Pop";
import { recipeService } from "../services/RecipeService";

export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      activeRecipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),
      editRecipe() {
        Modal.getOrCreateInstance('#recipeDetails').hide();
        Modal.getOrCreateInstance('#editRecipe').show();
      },
      async deleteRecipe() {
        try {
          const yes = await Pop.confirm('Delete this recipe?');
          if (!yes) { return }
          await recipeService.deleteRecipe(this.activeRecipe.id);
          Modal.getOrCreateInstance('#recipeDetails').hide();
        }
        catch (error) { Pop.error(error); }
      }
    };
  },
  components: { IngredientsList, Instructions }
};
</script>


<style lang="scss" scoped>
.card {
  height: 100%;
}

.text-grey {
  color: #80808080;
}

.text-danger {
  opacity: .86;
}

.category {
  top: 1rem;
  left: .7rem;
  background-color: #ffffffd9;
}
</style>