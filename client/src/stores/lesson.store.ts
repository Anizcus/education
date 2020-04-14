import { Module, ActionTree, GetterTree, MutationTree } from 'vuex';
import { LessonStoreModel, CategoryModel, TypeModel, LessonModel } from '@/models/stores/lesson.store.model';
import { LessonService } from '@/services/lesson.service';
import { IdServiceModel } from '@/models/services/id.service.model';

const namespaced = true;

const state: LessonStoreModel = {
   categories: [],
   types: [],
   lessons: []
};

const mutations: MutationTree<LessonStoreModel> = {
   insertCategories(state, categories: CategoryModel[]) {
      state.categories = categories;
   },
   insertTypes(state, types: TypeModel[]) {
      state.types = types;
   },
   insertLessons(state, lessons: LessonModel[]) {
      state.lessons = lessons;
   }
};

const getters: GetterTree<LessonStoreModel, {}> = {
   categories(state) {
      return state.categories;
   },
   types(state) {
      return state.types;
   },
   lessons(state) {
      return state.lessons;
   }
};

const actions: ActionTree<LessonStoreModel, {}> = {
   async getCategories(context) {
      return await LessonService.getCategories().then(response => {
         context.commit("insertCategories", response.names);

         return response.names;
      });
   },
   async getTypesByCategory(context, model: IdServiceModel) {
      return await LessonService.getTypesByCategory(model)
         .then(response => {
            context.commit("insertTypes", response.names);

            return response.names;
      });
   },
   async getPublishedLessonsByType(context, model: IdServiceModel) {
      return await LessonService.getPublishedLessonsByType(model)
         .then(response => {
            context.commit("insertLessons", response);

            return response;
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
