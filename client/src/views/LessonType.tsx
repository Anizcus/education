import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { TypeModel } from "@/models/stores/lesson.store.model";

@Component({
  methods: {
    ...mapActions("lesson", {
      getTypes: "getTypesByCategory"
    })
  },
  computed: {
    ...mapGetters("lesson", {
      types: "types"
    })
  }
})
class LessonType extends Vue {
  private getTypes!: ActionMethod;
  private types!: TypeModel[];
  private loading = true;

  public mounted() {
    this.getTypes({ id: Number(this.$route.params.id) })
      .then(res => {
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

    if (!this.types.length) {
      return <el-card style={{ textAlign: "center" }}>No data</el-card>;
    }

    const types = this.types.map((item: TypeModel) => {
      return (
        <el-card>
          <router-link to={`/lesson/type/${item.id}`}>
              <el-link type="primary" underline={false}>{item.name}</el-link>
          </router-link>
        </el-card>
      );
    });

    return (
      <el-row>
        <el-col>{types}</el-col>
      </el-row>
    );
  }
}

export default LessonType;
