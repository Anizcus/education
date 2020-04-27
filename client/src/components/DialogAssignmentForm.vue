<template>
  <el-dialog
    :title="`${modalState} an assignment`"
    :visible="assignmentModal"
    @close="onCancel"
    @open="onOpen"
  >
    <el-form v-if="modalState !== 'Delete'" :model="form" label-position="top">
      <el-form-item label="Description">
        <el-input
          type="textarea"
          v-model="form.description"
          :rows="5"
        ></el-input>
      </el-form-item>
      <el-form-item label="Answer">
        <el-input v-model="form.answer"></el-input>
      </el-form-item>
      <el-form-item label="Experience">
        <el-input-number v-model="form.experience" :min="1"></el-input-number>
      </el-form-item>
    </el-form>
    <div v-else>Are you sure want to delete question {{ data.index + 1 }}?</div>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">Cancel</el-button>
      <el-button type="primary" @click="onAction">{{ modalState }}</el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions, ActionMethod } from "vuex";

interface ModalData {
  description: string;
  answer: string;
  experience: number;
  index?: number;
}

@Component({
  methods: {
    ...mapActions("modal", {
      setAssignmentModalVisible: "setAssignmentModalVisible"
    }),
    ...mapActions("lesson", {
      createAssignment: "createAssignment",
      updateAssignment: "updateAssignment",
      deleteAssignment: "deleteAssignment"
    })
  },
  computed: {
    ...mapGetters("modal", {
      assignmentModal: "assignmentModalVisible",
      modalState: "modalState",
      data: "modalData"
    })
  }
})
class DialogAssignmentForm extends Vue {
  private setAssignmentModalVisible!: ActionMethod;
  private updateAssignment!: ActionMethod;
  private createAssignment!: ActionMethod;
  private deleteAssignment!: ActionMethod;
  private assignmentModal!: boolean;
  private data!: ModalData;
  private modalState!: string;

  private form: ModalData = {
    description: "",
    answer: "",
    experience: 1
  };

  private onAction() {
    const assignment = {
      id: 0,
      description: this.form.description,
      experience: this.form.experience,
      answer: this.form.answer
    };

    if (this.modalState == "Create") {
      this.createAssignment(assignment);
    }

    if (this.modalState == "Update") {
      assignment.id = this.data.index || 0;

      this.updateAssignment(assignment);
    }

    if (this.modalState == "Delete") {
      this.deleteAssignment(this.data.index);
    }

    this.setAssignmentModalVisible({
      visible: false,
      modalState: "Action"
    });
  }

  private onCancel() {
    this.setAssignmentModalVisible({
      visible: false,
      modalState: "Cancel"
    });

    this.form.description = "";
    this.form.answer = "";
    this.form.experience = 1;
  }

  private onOpen() {
    if (this.data) {
      this.form.description = this.data.description;
      this.form.answer = this.data.answer;
      this.form.experience = this.data.experience || 1;
    }
  }
}
export default DialogAssignmentForm;
</script>

<style lang="scss">
.el-input-number {
  width: 100%;
}
</style>
