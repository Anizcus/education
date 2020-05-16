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
  },
  updateType: (model: NameServiceModel): Promise<NameServiceModel> => {
    return Service.put("type", model);
  },
  updateCategory: (model: NameServiceModel): Promise<NameServiceModel> => {
    return Service.put("category", model);
  },
  createType: (model: NameServiceModel): Promise<NameServiceModel> => {
    return Service.post("type", model);
  },
  createCategory: (model: NameServiceModel): Promise<NameServiceModel> => {
    return Service.post("category", model);
  }
};

export { AdminService, NameGroupModel };
