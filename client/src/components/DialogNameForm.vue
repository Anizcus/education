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
      <el-form-item :label="data && data.labelName">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">{{ data && data.labelBack }}</el-button>
      <el-button
        :type="isCreate ? 'success' : 'danger'"
        @click="() => onAction()"
        :loading="loading"
        >{{ data && data.labelAction }}</el-button
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
  labelBack: string;
  labelName: string;
  labelAction: string;
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

  private onAction() {
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

  get isCreate() {
    return this.modalState == "Create";
  }
}
export default DialogNameForm;
</script>
