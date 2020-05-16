<template>
  <el-dialog
    :title="data && data.title"
    :visible="nameModal"
    @close="onClose"
    @open="onOpen"
  >
    <el-alert
      v-if="error"
      :title="error"
      type="error"
      :show-icon="true"
      @close="onCloseAlert"
    >
    </el-alert>
    <el-form :model="form" label-position="top">
      <el-form-item :label="`${data && data.entity} name`">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">Cancel</el-button>
      <el-button
        :type="modalState == 'Create' ? 'success' : 'danger'"
        @click="() => onAction(modalState == 'Create')"
        :loading="loading"
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
  id: number;
  title: string;
  name: string;
  entity: string;
  onAction: (id: number, name: string) => Promise<void>;
}

@Component({
  methods: {
    ...mapActions("modal", {
      setNameModalVisible: "setNameModalVisible"
    })
  },
  computed: {
    ...mapGetters("modal", {
      nameModal: "nameModalVisible",
      modalState: "modalState",
      data: "modalData"
    })
  }
})
class DialogNameForm extends Vue {
  private setNameModalVisible!: ActionMethod;
  private nameModal!: boolean;
  private modalState!: string;
  private data!: ModalData;
  private loading = false;
  private error = "";
  private form = {
    name: ""
  };

  private onOpen() {
    this.form.name = this.data.name || "";
  }

  private onClose() {
    this.setNameModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }

  private onCloseAlert() {
    this.error = "";
  }

  private onAction(state: boolean) {
    this.loading = true;
    this.data
      .onAction(this.data.id, this.form.name)
      .then(() => {
        this.loading = false;
        this.setNameModalVisible({
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
    this.setNameModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }
}
export default DialogNameForm;
</script>
