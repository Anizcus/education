import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";
import UserStore from "@/stores/user.store";
import LessonStore from "./lesson.store";
import ModalStore from "./modal.store";

Vue.use(Vuex);

const store: StoreOptions<{}> = {
  state: {},
  modules: {
    user: UserStore,
    lesson: LessonStore,
    modal: ModalStore
  }
};

export default new Vuex.Store<{}>(store);
