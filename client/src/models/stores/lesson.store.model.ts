interface CategoryModel {
  id: number;
  name: string;
}

interface TypeModel {
  id: number;
  name: string;
}

interface LessonListModel {
  id: number;
  name: string;
  ownerId: number;
  ownerName: string;
  state: string | null;
  badgeBase64: string;
}

interface LessonModel {
  id: number;
  name: string;
  description: string;
  ownerId: number;
  ownerName: string;
  state: string | null;
  badgeBase64: string;
  type: string;
  status: string;
  category: string;
  progress: string;
  assignments: AssignmentModel[];
}

interface AssignmentModel {
  id: number;
  description: string;
  experience: number;
  answer: string | null;
  progress: string;
}

interface LessonStatusModel {
  lessonId: number;
  isValid: boolean;
  status: string;
}

interface LessonStoreModel {
  categories: CategoryModel[];
  types: TypeModel[];
  lessons: LessonListModel[];
  lesson: LessonModel;
}

interface AssignmentAnswer {
  assignmentId: number;
  answer: boolean;
}

export {
  LessonStoreModel,
  CategoryModel,
  TypeModel,
  LessonModel,
  LessonListModel,
  AssignmentModel,
  LessonStatusModel,
  AssignmentAnswer
};
