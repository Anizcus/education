import { GetterTree, ActionTree, Module, MutationTree } from "vuex";

const namespaced = true;

interface ModalStoreModel {
   assignmentModal: boolean;
   lessonModal: boolean;
   data?: object;
   stateName: string;
}

interface ModalPayload {
   visible: boolean;
   data?: object;
   stateName: string;
}

const mutations: MutationTree<ModalStoreModel> = {
   toggleAssignment(state, payload: ModalPayload) {
      state.data = {... payload.data};
      state.stateName = payload.stateName;
      state.assignmentModal = payload.visible;
   },
   toggleLesson(state, payload: ModalPayload) {
      state.data = {... payload.data};
      state.stateName = payload.stateName;
      state.lessonModal = payload.visible;
      console.log(payload);
   },
};

const getters: GetterTree<ModalStoreModel, {}> = {
   assignmentModalVisible(state) {
      return state.assignmentModal;
   },
   lessonModalVisible(state) {
      return state.lessonModal;
   },
   modalState(state) {
      return state.stateName;
   },
   modalData(state) {
      return state.data;
   },
};

const actions: ActionTree<ModalStoreModel, {}> = {
   setAssignmentModalVisible(context, payload: ModalPayload) {
      context.commit("toggleAssignment", payload);
   },
   setLessonModalVisible(context, payload: ModalPayload) {
      context.commit("toggleLesson", payload);
   }
}

const state: ModalStoreModel = {
   assignmentModal: false,
   lessonModal: false,
   data: undefined,
   stateName: "",
}

const modalStore: Module<ModalStoreModel, {}> = {
   state,
   getters,
   actions,
   mutations,
   namespaced
};

export default modalStore;
