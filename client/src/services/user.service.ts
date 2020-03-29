import { Service } from "./service";
import { NameServiceModel } from "@/models/services/name.service.model";
import { IdServiceModel } from "@/models/services/id.service.model";
import { LoginServiceModel } from "@/models/services/login.service.model";
import { SessionServiceModel } from "@/models/services/session.service.model";
import { RegisterServiceModel } from "@/models/services/register.service.model";

const UserService = {
  get: (model: IdServiceModel) => {
    return Service.get<IdServiceModel, NameServiceModel>("user", {
      params: model
    });
  },
  login: (model: LoginServiceModel) => {
    return Service.post<LoginServiceModel, SessionServiceModel>("user/signin", {
      ...model
    });
  },
  register: (model: RegisterServiceModel) => {
    return Service.post<RegisterServiceModel, NameServiceModel>("user/signup", {
      ...model
    });
  }
};

export { UserService };
