<template>
  <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" :id="modalId + 'Label'">
            <slot name="modalTitle"></slot>
          </h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <slot name="modalBody"></slot>
        </div>
        <div class="modal-footer" v-if="showFooter">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <slot name="modalFooter">
            <button type="button" class="btn btn-primary">Save changes</button>
          </slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, onMounted } from 'vue';

export default {
  props: {
    modalId: { type: String },
    modalSize: { type: String, default: '' },
    showFooter: { type: Boolean, default: false }
  },

  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped></style>