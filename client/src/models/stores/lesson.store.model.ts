interface CategoryModel {
  id: number;
  name: string;
}

interface TypeModel {
  id: number;
  name: string;
}

interface LessonModel {
  id: number;
  name: string;
  ownerId: number;
  ownerName: string;
}

interface LessonStoreModel {
  categories: CategoryModel[];
  types: TypeModel[];
  lessons: LessonModel[];
}

export { LessonStoreModel, CategoryModel, TypeModel, LessonModel };
