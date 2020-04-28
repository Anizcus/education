import { Module, ActionTree, GetterTree, MutationTree } from "vuex";
import {
  LessonStoreModel,
  CategoryModel,
  TypeModel,
  LessonModel,
  LessonListModel,
  AssignmentModel,
  LessonStatusModel
} from "@/models/stores/lesson.store.model";
import { LessonService } from "@/services/lesson.service";
import { IdServiceModel } from "@/models/services/id.service.model";

const namespaced = true;

const state: LessonStoreModel = {
  categories: [],
  types: [],
  lessons: [],
  lesson: ({ assignments: [] } as unknown) as LessonModel
};

const mutations: MutationTree<LessonStoreModel> = {
  insertCategories(state, categories: CategoryModel[]) {
    state.categories = categories;
  },
  insertTypes(state, types: TypeModel[]) {
    state.types = types;
  },
  insertLessons(state, lessons: LessonListModel[]) {
    state.lessons = lessons;
  },
  insertLesson(state, lesson: LessonModel) {
    state.lesson = lesson;
  },
  insertLessonAssignment(state, model: AssignmentModel) {
    state.lesson.assignments.push(model);
  },
  updateLessonAssignment(state, model: AssignmentModel) {
    const id = model.id;

    state.lesson.assignments[id].id = id;
    state.lesson.assignments[id].description = model.description;
    state.lesson.assignments[id].answer = model.answer;
    state.lesson.assignments[id].experience = model.experience;
  },
  deleteLessonAssignment(state, index: number) {
    state.lesson.assignments.splice(index, 1);
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
  },
  lesson(state) {
    return state.lesson;
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
    return await LessonService.getTypesByCategory(model).then(response => {
      context.commit("insertTypes", response.names);

      return response.names;
    });
  },
  async getPublishedLessonsByType(context, model: IdServiceModel) {
    return await LessonService.getAllLessonsByType(model).then(response => {
      context.commit("insertLessons", response);

      return response;
    });
  },
  async getLessonById(context, model: IdServiceModel) {
    return await LessonService.getLesson(model).then(response => {
      context.commit("insertLesson", response);

      return response;
    });
  },
  async postLessonAssignments(context) {
    return await LessonService.postLessonAssignments(context.state.lesson).then(
      response => {
        console.log(response);
        return response;
      }
    );
  },
  async postLessonStatus(context, model: LessonStatusModel) {
    return await LessonService.postLessonStatus(model).then(
      response => {
        console.log(response);
        return response;
      }
    );
  },
  async startLesson(context) {
    return await LessonService.startLesson({ id: context.state.lesson.id }).then(
      response => {
        console.log(response);
        return response;
      }
    );
  },
  createAssignment(context, model: any) {
    context.commit("insertLessonAssignment", model);
  },
  updateAssignment(context, model: any) {
    context.commit("updateLessonAssignment", model);
  },
  deleteAssignment(context, index: number) {
    context.commit("deleteLessonAssignment", index);
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
