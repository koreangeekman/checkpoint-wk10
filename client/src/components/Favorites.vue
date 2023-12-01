<template>
  <div class="favorites fs-2" @click.stop="toggleFav()">
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
        const exists = AppState.favRecipes.find(r => r.id == props.recipeId);
        // logger.log('exists?', exists, props.routineId);
        if (exists) {
          return exists.favoriteId;
        }
        return false;
      }),

      async toggleFav() {
        try {
          if (this.favorite) {
            await favService.removeFavorite(this.favorite);
          } else {
            await favService.addFavorite(props.recipeId);
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