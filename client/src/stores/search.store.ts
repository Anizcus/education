import { GetterTree, ActionTree, Module, MutationTree } from "vuex";
import LessonList from "@/views/LessonList";
import { SearchModel, SearchService } from "@/services/search.service";

const namespaced = true;

interface SearchStore {
  lessons: LessonList[];
}

const mutations: MutationTree<SearchStore> = {
  setLessons(state, lessons: LessonList[]) {
    state.lessons = [...lessons];
  }
};

const getters: GetterTree<SearchStore, {}> = {
  lessons(state) {
    return state.lessons;
  }
};

const actions: ActionTree<SearchStore, {}> = {
  async getLessons(context, model: SearchModel) {
    return await SearchService.getLessons(model).then(
      (response: LessonList[]) => {
        context.commit("setLessons", response);

        return response;
      }
    );
  }
};

const state: SearchStore = {
  lessons: []
};

const searchStore: Module<SearchStore, {}> = {
  state,
  getters,
  actions,
  mutations,
  namespaced
};

export default searchStore;
