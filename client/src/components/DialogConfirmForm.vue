<template>
  <el-dialog
    :title="`${(data && data.title) || ''}`"
    :visible="confirmModal"
    @close="onClose"
  >
    <p>{{ `${(data && data.message) || ""}` }}</p>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">Cancel</el-button>
      <el-button type="success" :loading="loading" @click="() => onAction()"
        >{{ modalState }}
      </el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions, ActionMethod } from "vuex";

interface ModalData {
  title: string;
  message: string;
  onAction: () => Promise<void>;
}

@Component({
  methods: {
    ...mapActions("modal", {
      setConfirmModalVisible: "setConfirmModalVisible"
    })
  },
  computed: {
    ...mapGetters("modal", {
      confirmModal: "confirmModalVisible",
      modalState: "modalState",
      data: "modalData"
    })
  }
})
class DialogConfirmForm extends Vue {
  private setConfirmModalVisible!: ActionMethod;
  private confirmModal!: boolean;
  private modalState!: string;
  private data!: ModalData;
  private loading = false;

  private onClose() {
    this.setConfirmModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }

  private onAction() {
    this.loading = true;
    this.data
      .onAction()
      .then(() => {
        this.loading = false;
        this.setConfirmModalVisible({
          visible: false,
          data: undefined,
          stateName: this.modalState
        });
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private onCancel() {
    this.setConfirmModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }
}
export default DialogConfirmForm;
</script>
