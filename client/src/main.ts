import Vue from "vue";
import Main from "./Main.vue";
import Router from "./routes/router";
import Store from "./stores/store";
import { Layout } from "ant-design-vue";

Vue.config.productionTip = false;

Vue.use(Layout);

new Vue({
  router: Router,
  store: Store,
  render: Element => Element(Main)
}).$mount("main");
