import { Module, MutationTree, GetterTree, ActionTree } from "vuex";
import { UserStoreModel, SessionModel } from "@/models/stores/user.store.model";
import { UserService } from "@/services/user.service";

const namespaced = true;

const state: UserStoreModel = {
  session: undefined
};

const mutations: MutationTree<UserStoreModel> = {
  insert(state, session: SessionModel) {
    state.session = { ...session };
  },
  remove(state) {
    state.session = undefined;
  }
};

const getters: GetterTree<UserStoreModel, {}> = {
  getSession(state) {
    return state.session;
  }
};

const actions: ActionTree<UserStoreModel, {}> = {
  logout(context) {
    context.commit("remove");
  },
  online(context) {
    return UserService.online().then(response => {
      context.commit("insert", { id: response.id, name: response.name });
    });
  }
};

const userStore: Module<UserStoreModel, {}> = {
  state,
  getters,
  mutations,
  actions,
  namespaced
};

export default userStore;
