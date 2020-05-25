import { Service } from "./service";
import LessonList from '@/views/LessonList';

interface SearchModel {
  typeId: number;
  name: string;
}

const SearchService = {
  getLessons: (model: SearchModel): Promise<LessonList[]> => {
    return Service.post("search", model);
  },
};

export { SearchService, SearchModel };
