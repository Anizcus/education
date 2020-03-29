import { NameServiceModel } from "./name.service.model";

interface SessionServiceModel {
  user: NameServiceModel;
  session: string;
}

export { SessionServiceModel };
