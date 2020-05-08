import { GetterTree, ActionTree, Module, MutationTree } from "vuex";

const namespaced = true;

interface ModalStoreModel {
  assignmentModal: boolean;
  lessonModal: boolean;
  authorizeModal: boolean;
  confirmModal: boolean;
  answerModal: boolean;
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
    state.data = { ...payload.data };
    state.stateName = payload.stateName;
    state.assignmentModal = payload.visible;
  },
  toggleLesson(state, payload: ModalPayload) {
    state.data = { ...payload.data };
    state.stateName = payload.stateName;
    state.lessonModal = payload.visible;
  },
  toggleAuthorize(state, payload: ModalPayload) {
    state.data = { ...payload.data };
    state.stateName = payload.stateName;
    state.authorizeModal = payload.visible;
  },
  toggleAnswer(state, payload: ModalPayload) {
    state.data = { ...payload.data };
    state.stateName = payload.stateName;
    state.answerModal = payload.visible;
  },
  toggleConfirm(state, payload: ModalPayload) {
    state.data = { ...payload.data };
    state.stateName = payload.stateName;
    state.confirmModal = payload.visible;
  }
};

const getters: GetterTree<ModalStoreModel, {}> = {
  assignmentModalVisible(state) {
    return state.assignmentModal;
  },
  lessonModalVisible(state) {
    return state.lessonModal;
  },
  authorizeModalVisible(state) {
    return state.authorizeModal;
  },
  answerModalVisible(state) {
    return state.answerModal;
  },
  confirmModalVisible(state) {
    return state.confirmModal;
  },
  modalState(state) {
    return state.stateName;
  },
  modalData(state) {
    return state.data;
  }
};

const actions: ActionTree<ModalStoreModel, {}> = {
  setAssignmentModalVisible(context, payload: ModalPayload) {
    context.commit("toggleAssignment", payload);
  },
  setLessonModalVisible(context, payload: ModalPayload) {
    context.commit("toggleLesson", payload);
  },
  setAuthorizeModalVisible(context, payload: ModalPayload) {
    context.commit("toggleAuthorize", payload);
  },
  setAnswerModalVisible(context, payload: ModalPayload) {
    context.commit("toggleAnswer", payload);
  },
  setConfirmModalVisible(context, payload: ModalPayload) {
    context.commit("toggleConfirm", payload);
  }
};

const state: ModalStoreModel = {
  assignmentModal: false,
  confirmModal: false,
  lessonModal: false,
  authorizeModal: false,
  answerModal: false,
  data: undefined,
  stateName: ""
};

const modalStore: Module<ModalStoreModel, {}> = {
  state,
  getters,
  actions,
  mutations,
  namespaced
};

export default modalStore;
