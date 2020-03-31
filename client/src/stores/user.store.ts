import { Module, MutationTree, GetterTree, ActionTree } from "vuex";
import { UserStoreModel, SessionModel } from "@/models/stores/user.store.model";
import { UserService } from "@/services/user.service";
import { RegisterServiceModel } from "@/models/services/register.service.model";
import { LoginServiceModel } from "@/models/services/login.service.model";

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
  session(state) {
    return state.session;
  }
};

const actions: ActionTree<UserStoreModel, {}> = {
  logout(context) {
    localStorage.removeItem("session");
    context.commit("remove");
  },

  async online(context) {
    const session = localStorage.getItem("session");
    if (session) {
      return await UserService.online().then(response => {
        context.commit("insert", response);
      });
    } else {
      return Promise.reject();
    }
  },

  async register(context, model: RegisterServiceModel) {
    return await UserService.register(model);
  },

  async login(context, model: LoginServiceModel) {
    return await UserService.login(model).then(response => {
      localStorage.setItem("session", response.session);
      context.commit("insert", response.user);

      return response.user;
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
