interface CategoryModel {
   id: number;
   name: string;
}
 
 interface LessonStoreModel {
   categories?: CategoryModel[];
 }
 
 export { LessonStoreModel, CategoryModel };
 