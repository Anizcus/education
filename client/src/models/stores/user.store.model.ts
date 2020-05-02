import { LessonModel } from "./lesson.store.model";

interface SessionModel {
  id: number;
  name: string;
  role: string;
}

interface ProfileModel {
  id: number;
  name: string;
  role: string;
  level: number;
  experience: number;
  nextExperience: number;
  lessons: LessonModel[];
}

interface ProfileListModel {
  id: number;
  name: string;
  role: string;
  level: number;
}

interface UserStoreModel {
  session?: SessionModel;
  profile?: ProfileModel;
  users: ProfileListModel[];
}

export { UserStoreModel, SessionModel, ProfileModel, ProfileListModel };
