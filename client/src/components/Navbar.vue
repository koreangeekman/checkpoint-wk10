<template>
  <nav class="navbar navbar-expand-sm px-3 position-absolute w-100">
    <div class="d-flex justify-content-between align-items-start w-100 h-100 position-relative">
      <div class="pe-1">
        <i data-bs-toggle="modal" data-bs-target="#newRecipe" v-if="account?.id"
          class="selectable navItems p-0 px-3 btn fs-1 mdi mdi-plus-circle"></i>
      </div>
      <div class="w-100 d-flex justify-content-center pt-2 position-absolute title-bar">
        <RouterLink :to="{ name: 'Home' }">
          <div class="cover-title rounded px-3 pb-2 text-center">
            <p class="fs-1 mb-0 fw-bold text-light">All Spice</p>
            <p class="fs-5 mb-0 text-light">
              Cherish Your Family <br>
              And Their Cooking
            </p>
          </div>
        </RouterLink>
      </div>
      <div class="d-flex justify-content-end align-items-center navItems">
        <SearchComponent />
        <div class="mx-2 mx-md-3">
          <!-- <button class="btn text-light" @click="toggleTheme">
            <i class="mdi" :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i>
          </button> -->
        </div>
        <Login />
      </div>
    </div>
  </nav>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import SearchComponent from "./SearchComponent.vue";
import { AppState } from "../AppState";
import Login from './Login.vue';

export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      account: computed(() => AppState.account),
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login, SearchComponent }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.cover-title {
  background-color: #00000039;
  text-shadow: 0 0 5px grey;
  z-index: 0;
}

.title-bar {
  top: 5rem;
  transition: .3s;
}

.navItems {
  z-index: 1;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}

@media screen and (min-width: 990px) {
  .title-bar {
    top: 0;
  }
}
</style>
