import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { LessonListModel } from "@/models/stores/lesson.store.model";
import { SessionModel } from '@/models/stores/user.store.model';

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
    }),
    ...mapGetters("user", {
      session: "session"
    })
  }
})
class LessonList extends Vue {
  private getLessons!: ActionMethod;
  private setLessonModalVisible!: ActionMethod;
  private lessons!: LessonListModel[];
  private session!: SessionModel;
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

    const addLessonButton = this.session 
      && this.session.role === 'Teacher' ? (
        <el-button
        style="margin-bottom: 14px;"
        plain={true}
        type="primary"
        onClick={() => this.onLessonCreate()}>
        <span>Add a new lesson</span>
      </el-button>
    ) : '';

    if (!this.lessons || !this.lessons.length) {
      return (
        <el-row>
          {addLessonButton}
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
    const owned = this.session && this.lessons
      .filter(item => item.ownerId === this.session.id)
      .map(this.renderItem) || '';

    const tabCreated = this.session && this.session.role !== 'Student' ? (
        <el-tab-pane label={`Created (${created.length})`}>
          {created}
        </el-tab-pane>)
      : '';

    const tabWaiting = this.session && this.session.role === 'Administrator' ? (
        <el-tab-pane label={`Waiting (${waiting.length})`}>
          {waiting}
        </el-tab-pane>)
      : '';

    const tabRejected = this.session && this.session.role === 'Administrator' ? (
        <el-tab-pane label={`Rejected (${rejected.length})`}>
          {rejected}
        </el-tab-pane>)
      : '';

    const tabOwned = this.session && this.session.role === 'Teacher' ? (
        <el-tab-pane label={`Owned (${owned.length})`}>
          {owned}
        </el-tab-pane>)
      : '';

    const tabPublished = (
      <el-tab-pane label={`Published (${published.length})`}>
        {published}
      </el-tab-pane>);

    return (
      <el-row>
        <el-col>
          {addLessonButton}
          <el-tabs type="card" stretch={true}>
            {tabPublished}
            {tabCreated}
            {tabWaiting}
            {tabRejected}
            {tabOwned}
          </el-tabs>
        </el-col>
      </el-row>
    );
  }
}

export default LessonList;
