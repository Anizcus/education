import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { LessonListModel } from "@/models/stores/lesson.store.model";

@Component({
  methods: {
    ...mapActions("lesson", {
      getLessons: "getPublishedLessonsByType"
    }),
    ...mapActions("modal", {
      setLessonModalVisible: "setLessonModalVisible"
    })
  },
  computed: {
    ...mapGetters("lesson", {
      lessons: "lessons"
    })
  }
})
class LessonList extends Vue {
  private getLessons!: ActionMethod;
  private setLessonModalVisible!: ActionMethod;
  private lessons!: LessonListModel[];
  private loading = true;
  private dateOptions = {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: false
  };

  public mounted() {
    this.getLessons({ id: Number(this.$route.params.id) })
      .finally(() => {
        this.loading = false;
      });
  }

  private onLessonCreate() {
    this.setLessonModalVisible({
      visible: true,
      stateName: "Create"
    });
  }

  private renderItem(item: LessonListModel) {
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
                  fontSize: "30px"
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
                backgroundColor: "#DCDFE6"
              }}
            ></div>
          </el-col>
          <el-col span={19}>
            <router-link to={`/lesson/${item.id}`}>
              <el-link type="primary" underline={false}>
                Lesson “<i>{item.name}</i>”
              </el-link>
            </router-link>
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
          <el-card shadow="hover" style={{ textAlign: "center" }}>
            No data
          </el-card>
        </el-row>
      );
    }

    const published = this.lessons
      .filter(item => item.state === "Published")
      .map(this.renderItem);
    const created = this.lessons
      .filter(item => item.state === "Created")
      .map(this.renderItem);
    const waiting = this.lessons
      .filter(item => item.state === "Waiting")
      .map(this.renderItem);
    const rejected = this.lessons
      .filter(item => item.state === "Rejected")
      .map(this.renderItem);

    return (
      <el-row>
        <el-col>
          <el-button
            style="margin-bottom: 14px;"
            plain={true}
            type="primary"
            onClick={() => this.onLessonCreate()}
          >
            Add a new lesson
          </el-button>
          <el-tabs type="card" stretch={true}>
            <el-tab-pane label={`Published (${published.length})`}>
              {published}
            </el-tab-pane>
            <el-tab-pane label={`Created (${created.length})`}>
              {created}
            </el-tab-pane>
            <el-tab-pane label={`Waiting (${waiting.length})`}>
              {waiting}
            </el-tab-pane>
            <el-tab-pane label={`Rejected (${rejected.length})`}>
              {rejected}
            </el-tab-pane>
          </el-tabs>
        </el-col>
      </el-row>
    );
  }
}

export default LessonList;
