import Vue from "vue";
import Component from "vue-class-component";
import Layout from "./views/Layout.vue";
import { VNode } from "vue/types/umd";
import { mapActions, ActionMethod } from "vuex";

@Component({
  components: {
    "i-layout": Layout
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
    this.isOnline().finally(() => {
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
          <el-button loading="loading" type="info" circle={true}></el-button>
        </el-container>
      );
    } else {
      return <i-layout></i-layout>;
    }
  }
}

export default Application;
