import { Service } from "./service";
import { NameListServiceModel } from '@/models/services/name-list.service.model';

const LessonService = {
  getCategories: (): Promise<NameListServiceModel> => {
    return Service.get("lesson/categories");
  }
};

export { LessonService };
