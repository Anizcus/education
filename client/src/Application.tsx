import Vue from "vue";
import Component from "vue-class-component";
import Layout from "./views/Layout.vue";
import DialogAssignmentForm from "./components/DialogAssignmentForm.vue";
import DialogLessonForm from "./components/DialogLessonForm.vue";
import DialogAuthorizeForm from "./components/DialogAuthorizeForm.vue";
import DialogAnswerForm from "./components/DialogAnswerForm.vue";
import DialogConfirmForm from "./components/DialogConfirmForm.vue";
import DialogManageUserForm from "./components/DialogManageUserForm.vue";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod } from "vuex";

@Component({
  components: {
    "i-layout": Layout,
    "i-dialog-assignment-form": DialogAssignmentForm,
    "i-dialog-authorize-form": DialogAuthorizeForm,
    "i-dialog-lesson-form": DialogLessonForm,
    "i-dialog-answer-form": DialogAnswerForm,
    "i-dialog-confirm-form": DialogConfirmForm,
    "i-dialog-manage-user-form": DialogManageUserForm
  },
  methods: {
    ...mapActions("user", {
      isOnline: "online"
    })
  }
})
class Application extends Vue {
  private isOnline!: ActionMethod;
  private loading = true;

  public mounted() {
    this.isOnline()
    .catch((error) => console.error(error))
    .finally(() => {
      this.loading = false;
    });
  }

  public render(): VNode {
    if (this.loading) {
      return (
        <el-container
          style={{
            alignItems: "center",
            justifyContent: "center",
            height: "100%"
          }}
        >
          <el-button
            loading={this.loading}
            type="info"
            circle={true}
          ></el-button>
        </el-container>
      );
    } else {
      return (
        <div style="height: 100%;">
          <i-dialog-assignment-form></i-dialog-assignment-form>
          <i-dialog-lesson-form></i-dialog-lesson-form>
          <i-dialog-authorize-form></i-dialog-authorize-form>
          <i-dialog-answer-form></i-dialog-answer-form>
          <i-dialog-confirm-form></i-dialog-confirm-form>
          <i-dialog-manage-user-form></i-dialog-manage-user-form>
          <i-layout></i-layout>
        </div>
      );
    }
  }
}

export default Application;
