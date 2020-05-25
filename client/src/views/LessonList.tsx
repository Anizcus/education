import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { LessonListModel } from "@/models/stores/lesson.store.model";
import { LanguageModel } from "@/assets/i18n/language";
import TitleComponent from "@/components/TitleComponent.vue";

@Component({
  methods: {
    ...mapActions("lesson", {
      getLessons: "getPublishedLessonsByType"
    }),
    ...mapActions("language", {
      localTime: "getLocalTime"
    })
  },
  computed: {
    ...mapGetters("lesson", {
      lessons: "lessons"
    }),
    ...mapGetters("language", {
      language: "getTranslations"
    })
  },
  components: {
    "i-title": TitleComponent
  }
})
class LessonList extends Vue {
  private getLessons!: ActionMethod;
  private language!: LanguageModel;
  private localTime!: ActionMethod;
  private lessons!: LessonListModel[];
  private loading = true;

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
              alt={this.language.Badge}
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
                {this.language.Lesson} “<i>{item.name}</i>”
              </el-link>
            </router-link>
            <el-divider>
              <i class="el-icon-star-on"></i>
            </el-divider>
            <span>
              {this.language.Author} <b>{item.ownerName}</b>
            </span>
            <span style="float: right;">{this.localTime(item.modified)}</span>
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

    const title = <i-title title={this.language.Lessons}></i-title>;

    if (!this.lessons || !this.lessons.length) {
      return (
        <el-row>
          {title}
          <el-col style="border-bottom: 1px dashed #e6e6e6; margin-bottom: 14px; padding: 0 24px;">
            <el-card
              shadow="hover"
              style="text-align: center; margin-bottom: 14px;"
            >
              {this.language.NoData}
            </el-card>
          </el-col>
        </el-row>
      );
    }

    const published = this.lessons.map(this.renderItem);

    return (
      <el-row>
        {title}
        <el-col style="border-bottom: 1px dashed #e6e6e6; padding: 0 24px;">
          {published}
        </el-col>
      </el-row>
    );
  }
}

export default LessonList;
