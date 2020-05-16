import { Service } from "./service";
import { NameServiceModel } from "@/models/services/name.service.model";

interface NameGroupModel {
  id: number;
  label: string;
  options: NameServiceModel[];
}

const AdminService = {
  getTypes: (): Promise<NameGroupModel[]> => {
    return Service.get("type/all");
  },
  getCategories: (): Promise<NameGroupModel[]> => {
    return Service.get("category/all");
  }
};

export { AdminService, NameGroupModel };
