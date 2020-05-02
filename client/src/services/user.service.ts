import { Service } from "./service";
import { NameServiceModel } from "@/models/services/name.service.model";
import { IdServiceModel } from "@/models/services/id.service.model";
import { LoginServiceModel } from "@/models/services/login.service.model";
import { SessionServiceModel } from "@/models/services/session.service.model";
import { RegisterServiceModel } from "@/models/services/register.service.model";
import {
  ProfileModel,
  ProfileListModel
} from "@/models/stores/user.store.model";

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
  online: (): Promise<NameServiceModel> => {
    return Service.post("user/online");
  },
  getProfile: (model: IdServiceModel): Promise<ProfileModel> => {
    return Service.get("user/profile", {
      params: model
    });
  },
  getUsers: (): Promise<ProfileListModel[]> => {
    return Service.get("users");
  }
};

export { UserService };
