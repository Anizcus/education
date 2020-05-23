import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { CategoryModel } from "@/models/stores/lesson.store.model";
import TitleComponent from "@/components/TitleComponent.vue";
import { LanguageModel } from '@/assets/i18n/language';

@Component({
  methods: {
    ...mapActions("lesson", {
      getCategories: "getCategories"
    })
  },
  computed: {
    ...mapGetters("lesson", {
      categories: "categories"
    }),
    ...mapGetters("language", {
      language: "getTranslations",
    }),
  },
  components: {
    "i-title": TitleComponent
  }
})
class LessonCategory extends Vue {
  private getCategories!: ActionMethod;
  private categories!: CategoryModel[];
  private language!: LanguageModel;
  private loading = true;

  public mounted() {
    this.getCategories().finally(() => {
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

    const title = <i-title title={this.language.Categories}></i-title>;

    if (!this.categories.length) {
      return (
        <el-row>
          <el-col>
            {title}
            <el-card shadow="hover" style={{ textAlign: "center" }}>
              {this.language.NoData}
            </el-card>
          </el-col>
        </el-row>
      );
    }

    const categories = this.categories.map((item: CategoryModel) => {
      return (
        <el-card shadow="hover" style="margin-bottom: 14px;">
          <router-link to={`/lesson/category/${item.id}`}>
            <el-link type="primary" underline={false}>
              {item.name}
            </el-link>
          </router-link>
        </el-card>
      );
    });

    return (
      <el-row>
        {title}
        <el-col>{categories}</el-col>
      </el-row>
    );
  }
}

export default LessonCategory;
