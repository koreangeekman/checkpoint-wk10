<template>
  <div class="mx-3 d-flex align-items-center">
    <input v-model="search.category" type="search" id="search" name="category" class="form-control"
      placeholder="Search...">
    <i class="ms-2 fs-3 btn btn-primary py-0 px-1 mdi mdi-magnify" tabindex="0" @click="search()"></i>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref } from 'vue';
import Pop from "../utils/Pop";

export default {
  setup() {
    const search = ref({})

    return {
      search,

      async search() {
        try { }
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