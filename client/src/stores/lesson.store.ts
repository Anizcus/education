import { Module, ActionTree, GetterTree, MutationTree } from 'vuex';
import { LessonStoreModel, CategoryModel } from '@/models/stores/lesson.store.model';
import { LessonService } from '@/services/lesson.service';

const namespaced = true;

const state: LessonStoreModel = {
   categories: undefined
};

const mutations: MutationTree<LessonStoreModel> = {
   insert(state, categories: CategoryModel[]) {
      state.categories = categories;
   }
};

const getters: GetterTree<LessonStoreModel, {}> = {
   categories(state) {
      return state.categories;
   }
};

const actions: ActionTree<LessonStoreModel, {}> = {
   async getCategories(context) {
      return await LessonService.getCategories().then(response => {
         context.commit("insert", response.names);

         return response.names;
      });
   }
};

const lessonStore: Module<LessonStoreModel, {}> = {
   state,
   getters,
   mutations,
   actions,
   namespaced
};

export default lessonStore;
