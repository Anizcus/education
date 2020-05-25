<template>
  <el-dialog
    :title="data && data.labelTitle"
    :visible="assignmentModal"
    @close="onCancel"
    @open="onOpen"
  >
    <el-form v-if="modalState !== 'Delete'" :model="form" label-position="top">
      <el-form-item :label="language.Description">
        <el-input
          type="textarea"
          v-model="form.description"
          :rows="5"
        ></el-input>
      </el-form-item>
      <el-form-item :label="language.AssignmentAnswer">
        <el-input v-model="form.answer"></el-input>
      </el-form-item>
      <el-form-item :label="language.Points">
        <el-input-number v-model="form.experience" :min="1"></el-input-number>
      </el-form-item>
    </el-form>
    <div v-else>
      {{ language.DeleteQuestion}}
    </div>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">{{ data && data.labelBack }}</el-button>
      <el-button type="primary" @click="onAction">{{ data && data.labelAction }}</el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions, ActionMethod } from "vuex";
import { LanguageModel } from "../assets/i18n/language";

interface ModalData {
  description: string;
  answer: string;
  experience: number;
  index?: number;
  labelTitle: string;
  labelAction: string;
  labelBack: string;
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
    }),
    ...mapGetters("language", {
      language: "getTranslations"
    })
  }
})
class DialogAssignmentForm extends Vue {
  private setAssignmentModalVisible!: ActionMethod;
  private updateAssignment!: ActionMethod;
  private createAssignment!: ActionMethod;
  private deleteAssignment!: ActionMethod;
  private language!: LanguageModel;
  private assignmentModal!: boolean;
  private data!: ModalData;
  private modalState!: string;

  private form = {
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
      data: undefined,
      modalState: "Action"
    });
  }

  private onCancel() {
    this.setAssignmentModalVisible({
      visible: false,
      data: undefined,
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
