<template>
  <div class="mx-3 d-flex align-items-center flex-nowrap">
    <input v-model="search" type="search" id="search" name="category" class="form-control" placeholder="Search...">
    <i class="ms-2 fs-3 btn btn-primary py-0 px-1 mdi mdi-magnify" tabindex="0" @click="sendQuery()"></i>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { ref } from 'vue';
import { recipeService } from "../services/RecipeService";

export default {
  setup() {
    const search = ref('')

    return {
      search,

      async sendQuery() {
        try {
          await recipeService.getRecipes(search.value)
          search.value = '';
        }
        catch (error) { Pop.error(error); }
      }

    }
  }
};
</script>


<style lang="scss" scoped>
.mdi-magnify {
  line-height: 2rem;
}
</style>