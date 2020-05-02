import { Module, MutationTree, GetterTree, ActionTree } from "vuex";
import {
  UserStoreModel,
  SessionModel,
  ProfileModel,
  ProfileListModel
} from "@/models/stores/user.store.model";
import { UserService } from "@/services/user.service";
import { RegisterServiceModel } from "@/models/services/register.service.model";
import { LoginServiceModel } from "@/models/services/login.service.model";
import { Service } from "@/services/service";
import { IdServiceModel } from "@/models/services/id.service.model";

const namespaced = true;

const state: UserStoreModel = {
  session: undefined,
  profile: undefined,
  users: []
};

const mutations: MutationTree<UserStoreModel> = {
  insert(state, session: SessionModel) {
    state.session = { ...session };
  },
  remove(state) {
    state.session = undefined;
  },
  insertProfile(state, profile: ProfileModel) {
    state.profile = { ...profile };
  },
  insertUsers(state, profileList: ProfileListModel[]) {
    state.users = { ...profileList };
  }
};

const getters: GetterTree<UserStoreModel, {}> = {
  session(state) {
    return state.session;
  },
  profile(state) {
    return state.profile;
  },
  users(state) {
    return state.users;
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

      Service.defaults.headers = {
        Authorization: `Bearer ${response.session}`
      };

      context.commit("insert", response.user);

      return response.user;
    });
  },
  async getProfile(context, model: IdServiceModel) {
    return await UserService.getProfile(model).then(response => {
      context.commit("insertProfile", response);

      return response;
    });
  },
  async getUsers(context) {
    return await UserService.getUsers().then(response => {
      context.commit("insertUsers", response);

      return response;
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
