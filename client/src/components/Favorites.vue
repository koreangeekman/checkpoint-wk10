<template>
  <div class="favorites fs-2" @click.stop="toggleFav(recipeId)">
    <i v-if="favorite" class="text-warning mdi mdi-star"></i>
    <i v-else class="text-secondary mdi mdi-star-outline"></i>

  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { computed } from 'vue';
import { AppState } from '../AppState';
import { logger } from "../utils/Logger";
import { favService } from "../services/FavService";

export default {
  props: { recipeId: { type: Number } },

  setup(props) {
    return {

      favorite: computed(() => {
        const exists = AppState.favRecipes.find(r => r.id = props.recipeId);
        logger.log('exists?', exists, props.routineId);
        if (exists) {
          return true;
        }
        return false;
      }),

      async toggleFav(recipeId) {
        try {
          if (this.favorite) {
            await favService.removeFavorite(recipeId);
          } else {
            await favService.addFavorite(recipeId);
          }
        }
        catch (error) { Pop.error(error); }
      }

    }
  }
};
</script>


<style lang="scss" scoped>
i {
  line-height: 2rem;
}
</style>