<template>
  <div v-if="instructions.length > 0" class="rounded shadow card">
    <div class="bg-green rounded-top shadow text-center py-1 mb-2 fs-3">INSTRUCTIONS</div>
    <div class="mb-auto">
      <div class="list d-flex align-items-center px-2 m-1">
        <span class="d-flex w-100 pb-2 border-bottom">
          <p class="mb-0">Step #</p>
          <p class="mb-0 mx-2">●</p>
          <p class="mb-0">Instruction</p>
        </span>
      </div>
      <div class="list d-flex align-items-center pt-1 px-2 m-1" v-for="step in instructions">
        <span class="d-flex showHidden w-100">
          <p class="mb-0">{{ step.position }}</p>
          <p class="mb-0 mx-2">●</p>
          <p class="mb-0">{{ step.body }}</p>
        </span>
        <i class="text-danger hidden fs-5 mdi mdi-trash-can" type="button" v-if="creatorId == account.id"
          @click="removeStep(step.id)"></i>
      </div>
    </div>
    <hr class="m-0">
    <div class="d-flex align-items-center p-2" v-if="creatorId == account.id">
      <input v-model="stepForm.position" type="text" name="position" class="form-control w-50" maxlength="24" required
        placeholder="Position..">
      <p class="mb-0 mx-1">●</p>
      <input v-model="stepForm.body" type="text" name="name" class="form-control" maxlength="32" required
        placeholder="Instruction..">
      <i class="btn btn-primary fs-4 px-1 ms-1 mdi mdi-plus" tabindex="0" @click="addStep()" type="button"
        :class="(stepForm.position == '' || stepForm.position == null || stepForm.body == '' || stepForm.body == null) ? 'disabled' : ''"></i>
    </div>
  </div>
  <div v-else class="rounded shadow card">
    <div class="bg-green rounded-top shadow text-center py-1 fs-3">INSTRUCTIONS</div>
    <div class="list d-flex p-2 m-1">
      <p class="">{{ instructionsBlock }}</p>
    </div>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import { instructionStepService } from "../services/InstructionStepService";
import { logger } from "../utils/Logger";

export default {
  props: {
    instructionsBlock: { type: String },
    creatorId: { type: String }
  },

  setup() {
    const stepForm = ref({});

    return {
      stepForm,
      instructions: computed(() => AppState.instructions),
      account: computed(() => AppState.account),

      async addStep() {
        try {
          if (stepForm.value.position == '' || stepForm.value.position == null || stepForm.value.body == '' || stepForm.value.body == null) {
            logger.log('condition matched')
            return
          }
          stepForm.value.recipeId = AppState.activeRecipe.id;
          await instructionStepService.addStep(stepForm.value);
          stepForm.value = {};
        }
        catch (error) { Pop.error(error); }
      },
      async removeStep(stepId) {
        try {
          const yes = await Pop.confirm('Remove this step?');
          if (!yes) { return }
          await instructionStepService.removeStep(stepId);
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

.mdi-plus {
  line-height: 1.2rem;
}

.mdi-trash-can {
  opacity: .86;
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

.showHidden:hover+.hidden {
  visibility: visible;
  opacity: 1;
}
</style>