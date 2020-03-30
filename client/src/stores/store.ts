import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";
import UserStore from "@/stores/user.store";

Vue.use(Vuex);

const store: StoreOptions<{}> = {
  state: {},
  modules: {
    user: UserStore
  }
};

export default new Vuex.Store<{}>(store);
