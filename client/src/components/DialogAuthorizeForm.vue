<template>
  <el-dialog
    title="Authorize a lesson"
    :visible="authorizeModal"
    @close="onClose"
  >
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
      <el-button :type="modalState == 'Approve' ? 'success' : 'danger'" @click="() => onAction(modalState == 'Approve')"
        >{{modalState}}</el-button
      >
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { LessonService } from "../services/lesson.service";
import { mapGetters, mapActions, ActionMethod } from "vuex";

interface ModalData {
  lessonId: number;
}

@Component({
  methods: {
    ...mapActions("lesson", {
      postStatus: "postLessonStatus"
    }),
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
  };

  private onClose() {
    this.setAuthorizeModalVisible({
      visible: false,
      stateName: this.modalState
    });
  }

  private onAction(state: boolean) {
    this.postStatus({
      lessonId: this.data.lessonId,
      status: this.form.description,
      isValid: state
    });

    this.setAuthorizeModalVisible({
      visible: false,
      stateName: this.modalState
    });
  }

  private onCancel() {
    this.setAuthorizeModalVisible({
      visible: false,
      stateName: this.modalState
    });
  }
}
export default DialogAuthorizeForm;
</script>
