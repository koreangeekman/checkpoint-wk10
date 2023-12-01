<template>
  <div class="rounded shadow card">
    <div class="bg-green rounded-top shadow text-center py-1 fs-3">INSTRUCTIONS</div>
    <div class="list d-flex p-2 m-1">
      <p class="">{{ instructions }}</p>
    </div>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import { ingredientService } from "../services/IngredientService";

export default {
  props: { instructions: { type: String } },

  setup() {
    const ingredientForm = ref({});

    return {
      ingredientForm,
      ingredients: computed(() => AppState.ingredients),

      async addIngredients() {
        try { await ingredientService.addIngredient(); }
        catch (error) { Pop.error(error); }
      },
      async removeIngredients() {
        try { await ingredientService.addIngredient(); }
        catch (error) { Pop.error(error); }
      }

    }
  }
};
</script>


<style lang="scss" scoped>
.bg-green {
  background-color: green;
}
</style>