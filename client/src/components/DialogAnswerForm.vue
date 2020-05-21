<template>
  <el-dialog title="Atsakyti į klausimą." :visible="answerModal" @close="onClose">
    <el-form :model="form" label-position="top">
      <el-form-item :label="`Klausimas: ${(data && data.question) || ''}`">
        <el-input :clearable="true" v-model="form.answer"></el-input>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">Atgal</el-button>
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
  assignmentId: number;
  lessonId: number;
  question: string;
}

@Component({
  methods: {
    ...mapActions("lesson", {
      postAnswer: "postAnswer",
      getLesson: "getLessonById"
    }),
    ...mapActions("modal", {
      setAnswerModalVisible: "setAnswerModalVisible"
    })
  },
  computed: {
    ...mapGetters("modal", {
      answerModal: "answerModalVisible",
      modalState: "modalState",
      data: "modalData"
    })
  }
})
class DialogAnswerForm extends Vue {
  private setAnswerModalVisible!: ActionMethod;
  private postAnswer!: ActionMethod;
  private getLesson!: ActionMethod;
  private answerModal!: boolean;
  private modalState!: string;
  private data!: ModalData;
  private form = {
    answer: ""
  };
  private loading = false;

  private onClose() {
    this.setAnswerModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }

  private onAction() {
    this.loading = true;
    this.postAnswer({
      assignmentId: this.data.assignmentId,
      answer: this.form.answer
    })
      .then(() => {
        this.getLesson({ id: this.data.lessonId })
          .then(() => {
            this.loading = false;
            this.setAnswerModalVisible({
              visible: false,
              data: undefined,
              stateName: this.modalState
            });
          })
          .catch(() => {
            this.loading = false;
            this.setAnswerModalVisible({
              visible: false,
              data: undefined,
              stateName: this.modalState
            });
          });
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private onCancel() {
    this.setAnswerModalVisible({
      visible: false,
      data: undefined,
      stateName: this.modalState
    });
  }
}
export default DialogAnswerForm;
</script>
