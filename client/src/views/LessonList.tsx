import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { LessonModel } from "@/models/stores/lesson.store.model";
import DialogLessonForm from "@/components/DialogLessonForm.vue";

@Component({
  methods: {
    ...mapActions("lesson", {
      getLessons: "getPublishedLessonsByType"
    })
  },
  computed: {
    ...mapGetters("lesson", {
      lessons: "lessons"
    })
  },
  components: {
    "i-dialog-lesson-form": DialogLessonForm
  }
})
class LessonList extends Vue {
  private getLessons!: ActionMethod;
  private lessons!: LessonModel[];
  private loading = true;

  public mounted() {
    this.getLessons({ id: Number(this.$route.params.id) })
      .then(res => {
        console.log(res);
      })
      .finally(() => {
        this.loading = false;
      });
  }

  public render(): VNode {
    if (this.loading) {
      return <el-card loading={this.loading}></el-card>;
    }

    if (!this.lessons.length) {
      return (
        <el-row>
          <i-dialog-lesson-form></i-dialog-lesson-form>
          <el-card style={{ textAlign: "center" }}>No data</el-card>
        </el-row>
      );
    }

    const lessons = this.lessons.map((item: LessonModel) => {
      return <el-card>{item.name}</el-card>;
    });

    return (
      <el-row>
        <el-col>{lessons}</el-col>
      </el-row>
    );
  }
}

export default LessonList;
