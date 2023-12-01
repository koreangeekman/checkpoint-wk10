<template>
  <div class="rounded shadow card">
    <div class="bg-green rounded-top shadow text-center py-1 fs-3">INGREDIENTS</div>
    <div class="list d-flex p-2 m-1" v-for="ingredient in ingredients">
      <p class="me-1">{{ ingredient.quantity }} ●</p>
      <p class="me-2">{{ ingredient.name }}</p>
    </div>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import { ingredientService } from "../services/IngredientService";

export default {
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