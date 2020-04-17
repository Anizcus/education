import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { LessonModel } from "@/models/stores/lesson.store.model";
import DialogLessonForm from "@/components/DialogLessonForm.vue";

@Component({
  methods: {
    ...mapActions("lesson", {
      getLessons: "getPublishedLessonsByType",
    }),
  },
  computed: {
    ...mapGetters("lesson", {
      lessons: "lessons",
    }),
  },
  components: {
    "i-dialog-lesson-form": DialogLessonForm,
  },
})
class LessonList extends Vue {
  private getLessons!: ActionMethod;
  private lessons!: LessonModel[];
  private loading = true;
  private dateOptions = {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: false,
  };

  public mounted() {
    this.getLessons({ id: Number(this.$route.params.id) })
      .then((res) => {
        console.log(res);
      })
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
    }

    if (!this.lessons.length) {
      return (
        <el-row>
          <el-card shadow="hover" style={{ textAlign: "center" }}>No data</el-card>
        </el-row>
      );
    }

    const lessons = this.lessons.map((item: LessonModel) => {
      return (
        <el-card shadow="hover" style="margin-bottom: 14px;">
          <el-row>
            <el-col span={4}>
              <el-image
                src={item.badgeBase64}
                alt="Badge"
                style="width: 100px; height: 100px"
              >
                <div
                  slot="error"
                  style={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    width: "100%",
                    height: "100%",
                    background: "#f5f7fa",
                    color: "#909399",
                    fontSize: "30px",
                  }}
                >
                  <i class="el-icon-picture-outline"></i>
                </div>
              </el-image>
            </el-col>
            <el-col span={1} style="height: 100px;">
              <div
                style={{
                  width: "1px",
                  height: "100%",
                  margin: "0 8px",
                  verticalAlign: "middle",
                  position: "relative",
                  backgroundColor: "#DCDFE6",
                }}
              ></div>
            </el-col>
            <el-col span={19}>
              <span>
                Lesson <i>{item.name}</i>
              </span>
              <el-divider>
                <i class="el-icon-star-on"></i>
              </el-divider>
              <span>
                Author <b>{item.ownerName}</b>
              </span>
              <span style="float: right;">
                {new Date().toLocaleDateString(undefined, this.dateOptions)}
              </span>
            </el-col>
          </el-row>
        </el-card>
      );
    });
    return (
      <el-row>
        <el-col>
          <i-dialog-lesson-form></i-dialog-lesson-form>
          <el-tabs type="card" stretch={true}>
            <el-tab-pane label="Published"></el-tab-pane>
            <el-tab-pane label={`Created (${lessons.length})`}>{lessons}</el-tab-pane>
            <el-tab-pane label="Waiting"></el-tab-pane>
            <el-tab-pane label="Rejected"></el-tab-pane>
          </el-tabs>
        </el-col>
      </el-row>
    );
  }
}

export default LessonList;
