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
  Card,
  Select,
  Dialog,
  Option,
  Upload,
  Image,
  Divider,
  Tabs,
  TabPane,
  Collapse,
  InputNumber,
  CollapseItem
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
Vue.use(Select);
Vue.use(Dialog);
Vue.use(Option);
Vue.use(Upload);
Vue.use(Image);
Vue.use(Divider);
Vue.use(Tabs);
Vue.use(TabPane);
Vue.use(Collapse);
Vue.use(CollapseItem);
Vue.use(InputNumber);

new Vue({
  router: Router,
  store: Store,
  render: element => element(Application)
}).$mount("main");
