import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";
import UserStore from "@/stores/user.store";
import LessonStore from './lesson.store';

Vue.use(Vuex);

const store: StoreOptions<{}> = {
  state: {},
  modules: {
    user: UserStore,
    lesson: LessonStore
  }
};

export default new Vuex.Store<{}>(store);
