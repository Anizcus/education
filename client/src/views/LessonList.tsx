import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { LessonListModel } from "@/models/stores/lesson.store.model";

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
  }
})
class LessonList extends Vue {
  private getLessons!: ActionMethod;
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

  public created() {
    this.getLessons({ id: Number(this.$route.params.id) }).finally(() => {
      this.loading = false;
    });
  }

  private renderItem(item: LessonListModel) {
    return (
      <el-card shadow="hover" style="margin-bottom: 14px;">
        <el-row>
          <el-col span={4}>
            <el-image
              src={item.badgeBase64}
              alt="Ženkliukas"
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
                Pamoka “<i>{item.name}</i>”
              </el-link>
            </router-link>
            <el-divider>
              <i class="el-icon-star-on"></i>
            </el-divider>
            <span>
              Autorius <b>{item.ownerName}</b>
            </span>
            <span style="float: right;">
              {new Date(item.modified).toLocaleDateString(
                'lt-LT',
                this.dateOptions
              )}
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

    if (!this.lessons || !this.lessons.length) {
      return (
        <el-row>
          <el-card shadow="hover" style={{ textAlign: "center" }}>
            Nėra pamokų!
          </el-card>
        </el-row>
      );
    }

    const published = this.lessons.map(this.renderItem);

    return (
      <el-row>
        <el-col>{published}</el-col>
      </el-row>
    );
  }
}

export default LessonList;
