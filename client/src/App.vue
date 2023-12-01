<template>
  <div class="w-100 shadow mb-3">
    <img alt="cover image" class="coverImg"
      src="https://images.unsplash.com/photo-1556909211-36987daf7b4d?q=80&w=1280&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" />
  </div>
  <header class="position-absolute w-100">
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 64px);
}

.coverImg {
  width: 100%;
  height: 32.4rem;
  object-fit: cover;
  object-position: bottom;
  /* padding-bottom: 0rem; */
  margin-bottom: -6.4rem;
  clip-path: inset(0 0 6.4rem 0);
  z-index: -1;
}
</style>
