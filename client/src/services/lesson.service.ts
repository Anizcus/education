import { Service } from "./service";
import { NameListServiceModel } from "@/models/services/name-list.service.model";
import { IdServiceModel } from "@/models/services/id.service.model";
import {
  LessonModel,
  LessonListModel,
  LessonStatusModel,
  AssignmentAnswer
} from "@/models/stores/lesson.store.model";
import { NameServiceModel } from "@/models/services/name.service.model";

const LessonService = {
  getCategories: (): Promise<NameListServiceModel> => {
    return Service.get("lesson/categories");
  },
  getTypesByCategory: (
    model: IdServiceModel
  ): Promise<NameListServiceModel> => {
    return Service.get("lesson/types", {
      params: model
    });
  },
  getPublishedLessonsByType: (
    model: IdServiceModel
  ): Promise<LessonModel[]> => {
    return Service.get("lesson/published", {
      params: model
    });
  },
  getLessonListForAdmin: (): Promise<LessonListModel[]> => {
    return Service.get("lesson/admin");
  },
  getLessonListForTeacher: (): Promise<LessonListModel[]> => {
    return Service.get("lesson/teacher");
  },
  postLesson: (model: FormData): Promise<NameServiceModel> => {
    return Service.post("lesson", model);
  },
  putLesson: (model: FormData): Promise<NameServiceModel> => {
    return Service.put("lesson", model);
  },
  getLesson: (model: IdServiceModel): Promise<LessonModel> => {
    return Service.get("lesson", {
      params: model
    });
  },
  getAllLessonsByType: (model: IdServiceModel): Promise<LessonListModel[]> => {
    return Service.get("lesson/all", {
      params: model
    });
  },
  startLesson: (model: IdServiceModel): Promise<NameServiceModel> => {
    return Service.post("lesson/start", model);
  },
  postLessonAssignments: (model: LessonModel): Promise<NameServiceModel> => {
    return Service.post("lesson/assignment", model);
  },
  postLessonStatus: (model: LessonStatusModel): Promise<NameServiceModel> => {
    return Service.post("lesson/status", model);
  },
  postAssignmentAnswer: (
    model: AssignmentAnswer
  ): Promise<NameServiceModel> => {
    return Service.post("lesson/assignment/answer", model);
  }
};

export { LessonService };
