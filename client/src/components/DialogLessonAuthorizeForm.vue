<template>
    <el-dialog
      title="Authorize a lesson"
      :visible="assignmentModal"
      @close="onClose"
    >
      <el-form :model="form" label-position="top">
        <el-form-item label="Description">
          <el-input
            type="textarea"
            v-model="form.description"
            :rows="5"
          ></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="onCancel">Cancel</el-button>
        <el-button type="danger" @click="() => onAction(false)">Reject</el-button>
        <el-button type="primary" @click="() => onAction(true)">Approve</el-button>
      </span>
    </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { LessonService } from "../services/lesson.service";
import { mapGetters, mapActions, ActionMethod } from "vuex";

@Component({
  methods: {
    ...mapActions("modal", {
      setAssignmentModalVisible: "setAssignmentModalVisible",
    }),
  },
  computed: {
    ...mapGetters("modal", {
      assignmentModal: "assignmentModalVisible",
    }),
  },
})
class DialogLessonAuthorizeForm extends Vue {
  private setAssignmentModalVisible!: ActionMethod;
  private lessonAuthorizeModal!: boolean;
  private form = {
    description: "",
  };

  private onClose() {
    this.setAssignmentModalVisible(false);
  }

  private onAction(state: boolean) {
    this.setAssignmentModalVisible(false);
  }

  private onCancel() {
    this.setAssignmentModalVisible(false);
  }
}
export default DialogLessonAuthorizeForm;
</script>