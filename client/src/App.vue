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

  <ModalComponent :modalId="'newRecipe'" :modalSize="'modal-lg'" :showHeader="true">
    <template #modalTitle>New Recipe</template>
    <template #modalBody>
      <RecipeForm />
    </template>
  </ModalComponent>

  <ModalComponent :modalId="'editRecipe'" :modalSize="'modal-lg'" :showHeader="true">
    <template #modalTitle>Edit Recipe</template>
    <template #modalBody>
      <RecipeForm :edit="true" />
    </template>
  </ModalComponent>
</template>


<script>
import Navbar from './components/Navbar.vue'
import ModalComponent from "./components/ModalComponent.vue";
import RecipeForm from "./components/RecipeForm.vue";

export default {
  setup() {
    return {}
  },
  components: { Navbar, ModalComponent, RecipeForm }
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
  object-position: center;
  margin-bottom: -3.2rem;
  clip-path: inset(0 0 3.2rem 0);
  z-index: -1;
}
</style>
