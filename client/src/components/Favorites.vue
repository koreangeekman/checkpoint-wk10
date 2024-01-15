<template>
  <div class="favorites fs-2" @click.stop="toggleFav()" v-if="account?.id">
    <i v-if="favorite" class="text-warning mdi mdi-star"></i>
    <i v-else class="text-secondary mdi mdi-star-outline"></i>
  </div>
  <div class="mx-1" v-else></div>
</template>


<script>
import Pop from "../utils/Pop";
import { computed } from 'vue';
import { AppState } from '../AppState';
import { favService } from "../services/FavService";

export default {
  props: { recipeId: { type: Number } },

  setup(props) {
    return {
      account: computed(() => AppState.account),

      favorite: computed(() => {
        const exists = AppState.favRecipes.find(r => r.id == props.recipeId);
        if (exists) { return exists.favoriteId; }
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