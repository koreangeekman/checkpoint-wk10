<template>
  <div class="rounded shadow card">
    <div class="bg-green rounded-top shadow text-center py-1 mb-2 fs-3">INGREDIENTS</div>
    <div class="mb-auto">
      <div class="list d-flex align-items-center px-2 m-1">
        <span class="d-flex w-100 pb-2 border-bottom">
          <p class="mb-0">Quantity</p>
          <p class="mb-0 mx-2">●</p>
          <p class="mb-0">Ingredient</p>
        </span>
      </div>
      <div class="list d-flex align-items-center pt-1 px-2 m-1" v-for="ingredient in ingredients">
        <span class="d-flex showHidden w-100">
          <p class="mb-0">{{ ingredient.quantity }}</p>
          <p class="mb-0 mx-2">●</p>
          <p class="mb-0">{{ ingredient.name }}</p>
        </span>
        <i class="text-grey hidden mx-2 fs-5 mdi mdi-pencil" type="button" @click="editIngredient(ingredient)"
          v-if="creatorId == account.id"></i>
        <i class="text-danger hidden fs-5 mdi mdi-trash-can" type="button" @click="removeIngredient(ingredient.id)"
          v-if="creatorId == account.id"></i>
      </div>
    </div>
    <hr class="m-0">
    <div v-if="creatorId == account.id">
      <form @submit.prevent="submitForm()" class="d-flex align-items-center p-2">
        <input v-model="ingredientForm.quantity" type="text" name="quantity" class="form-control w-50" maxlength="24"
          required placeholder="Amount..">
        <p class="mb-0 mx-1">●</p>
        <input v-model="ingredientForm.name" type="text" name="name" class="form-control" maxlength="32" required
          placeholder="Ingredient..">
        <button class="btn btn-primary fs-4 px-1 ms-1 mdi mdi-plus" tabindex="0" type="submit"
          :class="(ingredientForm.name == '' || ingredientForm.name == null || ingredientForm.quantity == '' || ingredientForm.quantity == null) ? 'disabled' : ''"></button>
      </form>
    </div>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import { ingredientService } from "../services/IngredientService";
import { logger } from "../utils/Logger";

export default {
  props: {
    recipeId: { type: Number },
    creatorId: { type: String }
  },

  setup(props) {
    const ingredientForm = ref({});

    return {
      ingredientForm,
      ingredients: computed(() => AppState.ingredients),
      account: computed(() => AppState.account),

      editIngredient(ingredientObj) {
        ingredientForm.value = ingredientObj;
      },

      clearFormData() {
        ingredientForm.value = {};
      },
      submitForm() {
        if (ingredientForm.value.id) {
          this.updateIngredient(ingredientForm.value);
        } else {
          this.addIngredient(ingredientForm.value);
        }
      },
      incompleteFormCheck() {
        if (ingredientForm.value.name == '' ||
          ingredientForm.value.name == null ||
          ingredientForm.value.quantity == '' ||
          ingredientForm.value.quantity == null) { return true }
      },
      async updateIngredient() {
        try {
          if (this.incompleteFormCheck()) { return }
          ingredientForm.value.recipeId = props.recipeId;
          await ingredientService.updateIngredient(ingredientForm.value);
          ingredientForm.value = {};
        }
        catch (error) { Pop.error(error); }
      },
      async addIngredient() {
        try {
          if (this.incompleteFormCheck()) { return }
          ingredientForm.value.recipeId = props.recipeId;
          await ingredientService.addIngredient(ingredientForm.value);
          ingredientForm.value = {};
        }
        catch (error) { Pop.error(error); }
      },
      async removeIngredient(ingredientId) {
        try {
          const yes = await Pop.confirm('Remove this ingredient?');
          if (!yes) { return }
          await ingredientService.removeIngredient(ingredientId);
        }
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

.text-grey {
  color: #80808080;
}

.mdi-plus {
  line-height: 1.2rem;
}

.mdi-trash-can {
  opacity: .86;
}

.mdi-pencil,
.mdi-trash-can {
  line-height: 1.4rem;
}

.hidden {
  opacity: 0;
  transition: .25s;
}

.hidden:hover {
  visibility: visible;
  opacity: 1;
}

.showHidden:hover~.hidden {
  visibility: visible;
  opacity: 1;
}
</style>