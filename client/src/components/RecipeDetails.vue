<template>
  <div class="container-fluid">
    <section class="row" v-if="activeRecipe?.id">
      <div class="col-12 col-md-4 py-5">
        <img :src="activeRecipe.img" :alt="activeRecipe.name" class="img-fluid">
      </div>
      <div class="col-12 col-md-8 py-5">
        <section class="row">
          <span class="d-flex align-items-center justify-content-between mb-2">
            <span class="">
              <p class="mb-0 fw-bold fs-4">{{ activeRecipe.title }}</p>
              <p class="mb-0">{{ activeRecipe.subtitle }}</p>
            </span>
            <span class="d-flex" v-if="activeRecipe.creatorId == account?.id">
              <i class="btn selectable py-0 px-2 text-grey fs-3 mdi mdi-pencil" @click="editRecipe()"></i>
              <i class="btn selectable py-0 px-2 text-danger fs-3 mdi mdi-delete-forever" @click="deleteRecipe()"></i>
            </span>
          </span>
          <div class="col-12 col-md-6 p-3">
            <div class="card p-2">
              {{ activeRecipe.instructions }}
            </div>
          </div>
          <div class="col-12 col-md-6 p-3">
            <div class="card p-2">
              {{ ingredients }}
            </div>
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

export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      activeRecipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),

      editRecipe() {
        Modal.getOrCreateInstance('#recipeDetails').hide();
        Modal.getOrCreateInstance('#editRecipe').show();
      }
    }
  }
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
</style>