import { Service } from "./service";
import { NameServiceModel } from "@/models/services/name.service.model";
import { IdServiceModel } from "@/models/services/id.service.model";
import { LoginServiceModel } from "@/models/services/login.service.model";
import { SessionServiceModel } from "@/models/services/session.service.model";
import { RegisterServiceModel } from "@/models/services/register.service.model";
import {
  ProfileModel,
  ProfileListModel,
  SessionModel
} from "@/models/stores/user.store.model";
import {
  RoleRequestModel,
  ModifyUserRequestModel
} from "@/models/services/role.request.model";

const UserService = {
  get: (model: IdServiceModel): Promise<NameServiceModel> => {
    return Service.get("user", {
      params: model
    });
  },
  login: (model: LoginServiceModel): Promise<SessionServiceModel> => {
    return Service.post("user/signin", {
      ...model
    });
  },
  register: (model: RegisterServiceModel): Promise<NameServiceModel> => {
    return Service.post("user/signup", {
      ...model
    });
  },
  online: (): Promise<SessionModel> => {
    return Service.post("user/online");
  },
  getProfile: (model: IdServiceModel): Promise<ProfileModel> => {
    return Service.get("user/profile", {
      params: model
    });
  },
  getUsers: (): Promise<ProfileListModel[]> => {
    return Service.get("users");
  },
  getRoles: (model: RoleRequestModel): Promise<NameServiceModel[]> => {
    return Service.get("user/roles", {
      params: model
    });
  },
  postUserModify(model: ModifyUserRequestModel) {
    return Service.post("user/modify/status", {
      ...model
    });
  }
};

export { UserService };
