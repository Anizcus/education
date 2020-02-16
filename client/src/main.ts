import Vue from "vue";
import Main from "./main.vue";
import Router from "./routes/router";
import Store from "./stores/store";
import Ant from "ant-design-vue";
import "ant-design-vue/dist/antd.css";

Vue.config.productionTip = false;

Vue.use(Ant);

new Vue({
  store: Store,
  router: Router,
  render: application => application(Main)
}).$mount("main");
