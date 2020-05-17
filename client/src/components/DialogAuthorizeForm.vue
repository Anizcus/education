<template>
  <el-dialog
    title="Authorize a lesson"
    :visible="authorizeModal"
    @close="onClose"
  >
  <el-alert
      v-if="error"
      :title="error"
      type="error"
      :show-icon="true"
      @close="onCloseAlert"
    ></el-alert>
    <el-form :model="form" label-position="top">
      <el-form-item :label="`${modalState} status description (optional)`">
        <el-input
          type="textarea"
          v-model="form.description"
          :rows="5"
        ></el-input>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">Cancel</el-button>
      <el-button
        :type="modalState == 'Approve' ? 'success' : 'danger'"
        @click="() => onAction(modalState == 'Approve')"
        >{{ modalState }}</el-button
      >
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions, ActionMethod } from "vuex";

interface ModalData {
  lessonId: number;
  onAction: (id: number, status: string, valid: boolean) => Promise<void>;
}

@Component({
  methods: {
    ...mapActions("modal", {
      setAuthorizeModalVisible: "setAuthorizeModalVisible"
    })
  },
  computed: {
    ...mapGetters("modal", {
      authorizeModal: "authorizeModalVisible",
      modalState: "modalState",
      data: "modalData"
    })
  }
})
class DialogAuthorizeForm extends Vue {
  private setAuthorizeModalVisible!: ActionMethod;
  private postStatus!: ActionMethod;
  private authorizeModal!: boolean;
  private modalState!: string;
  private data!: ModalData;
  private form = {
    description: ""
  }
  private loading = false;
  private error = "";

  private onClose() {
    this.error = "";
    this.setAuthorizeModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }

  private onCloseAlert() {
    this.error = "";
  }

  private onAction(state: boolean) {
    this.error = "";
    this.loading = true;
    this.data
      .onAction(this.data.lessonId, this.form.description, state)
      .then(() => {
        this.loading = false;
        this.setAuthorizeModalVisible({
          visible: false,
          data: undefined,
          stateName: this.modalState
        });
      })
      .catch(error => {
        this.loading = false;
        this.error = error;
      });
  }

  private onCancel() {
    this.error = "";
    this.setAuthorizeModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }
}
export default DialogAuthorizeForm;
</script>
