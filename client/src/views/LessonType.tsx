import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { TypeModel } from "@/models/stores/lesson.store.model";
import { LanguageModel } from "@/assets/i18n/language";
import TitleComponent from "@/components/TitleComponent.vue";

@Component({
  methods: {
    ...mapActions("lesson", {
      getTypes: "getTypesByCategory"
    })
  },
  computed: {
    ...mapGetters("lesson", {
      types: "types"
    }),
    ...mapGetters("language", {
      language: "getTranslations"
    })
  },
  components: {
    "i-title": TitleComponent
  }
})
class LessonType extends Vue {
  private getTypes!: ActionMethod;
  private language!: LanguageModel;
  private types!: TypeModel[];
  private loading = true;

  public mounted() {
    this.getTypes({ id: Number(this.$route.params.id) }).finally(() => {
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

    const title = <i-title title={this.language.LessonTypes}></i-title>;

    if (!this.types.length) {
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

    const types = this.types.map((item: TypeModel) => {
      return (
        <el-card shadow="hover" style="margin-bottom: 14px;">
          <router-link to={`/lesson/type/${item.id}`}>
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
        <el-col style="border-bottom: 1px dashed #e6e6e6; padding: 0 24px;">
          {types}
        </el-col>
      </el-row>
    );
  }
}

export default LessonType;
