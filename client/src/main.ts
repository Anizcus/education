import Vue from "vue";
import Application from "./Application";
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
  Col,
  Menu,
  MenuItem,
  Link,
  Alert,
  Card
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
Vue.use(Menu);
Vue.use(MenuItem);
Vue.use(Link);
Vue.use(Alert);
Vue.use(Card);

new Vue({
  router: Router,
  store: Store,
  render: element => element(Application)
}).$mount("main");
