import { Service } from "./service";
import { NameListServiceModel } from '@/models/services/name-list.service.model';
import { IdServiceModel } from '@/models/services/id.service.model';
import { LessonModel } from '@/models/stores/lesson.store.model';

const LessonService = {
  getCategories: (): Promise<NameListServiceModel> => {
    return Service.get("lesson/categories");
  },
  getTypesByCategory: (model: IdServiceModel): Promise<NameListServiceModel> => {
    return Service.get("lesson/types", {
      params: model
    });
  },
  getPublishedLessonsByType: (model: IdServiceModel): Promise<LessonModel[]> => {
    return Service.get("lesson/published", {
      params: model
    });
  }
};

export { LessonService };
