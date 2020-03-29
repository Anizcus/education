import Vue from "vue";
import Application from "./Main.vue";
import Router from "./routes/router";
import Store from "./stores/store";

import {
  Container,
  Header,
  Main,
  Footer,
  Button,
  Aside,
  Form,
  FormItem,
  Input,
  Row,
  Col
} from "element-ui";

Vue.config.productionTip = false;

Vue.use(Container);
Vue.use(Header);
Vue.use(Main);
Vue.use(Footer);
Vue.use(Button);
Vue.use(Aside);
Vue.use(Form);
Vue.use(FormItem);
Vue.use(Input);
Vue.use(Row);
Vue.use(Col);

new Vue({
  router: Router,
  store: Store,
  render: element => element(Application)
}).$mount("main");
