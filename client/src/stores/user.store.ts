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
import { NameServiceModel } from "@/models/services/name.service.model";
import {
  RoleRequestModel,
  ModifyUserRequestModel
} from "@/models/services/role.request.model";

const namespaced = true;

const state: UserStoreModel = {
  session: undefined,
  roles: [],
  profile: undefined,
  users: []
};

const mutations: MutationTree<UserStoreModel> = {
  insert(state, session: SessionModel) {
    state.session = { ...session };
  },
  remove(state) {
    state.session = undefined;
    state.profile = undefined;
  },
  insertProfile(state, profile: ProfileModel) {
    state.profile = { ...profile };
  },
  insertUsers(state, profileList: ProfileListModel[]) {
    state.users = [...profileList];
  },
  insertRoles(state, roleList: NameServiceModel[]) {
    state.roles = [...roleList];
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
  },
  roles(state) {
    return state.roles;
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
      return await UserService.online().then((response: SessionModel) => {
        context.commit("insert", response);

        return response;
      });
    } else {
      context.commit("remove");

      return Promise.reject("You are not logged in!");
    }
  },

  async register(context, model: RegisterServiceModel) {
    return await UserService.register(model);
  },

  async login(context, model: LoginServiceModel) {
    return await UserService.login(model).then(response => {
      localStorage.setItem("session", response.session);
      context.commit("insert", {
        id: response.id,
        name: response.name,
        role: response.role
      });

      Service.defaults.headers = {
        Authorization: `Bearer ${response.session}`
      };

      return response;
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
  },
  async getRoles(context, model: RoleRequestModel) {
    return await UserService.getRoles(model).then(response => {
      context.commit("insertRoles", response);

      return response;
    });
  },
  async modifyStatus(context, model: ModifyUserRequestModel) {
    return await UserService.postUserModify(model).then(response => {
      context.dispatch("getUsers");

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
