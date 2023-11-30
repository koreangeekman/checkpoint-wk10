<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-4 text-center p-5" v-for="recipe in recipes">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
  </div>
</template>

<script>
import { onMounted } from "vue";
import RecipeCard from "../components/RecipeCard.vue";
import Pop from "../utils/Pop";

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

    };
  },
  components: { RecipeCard }
}
</script>

<style scoped lang="scss"></style>
