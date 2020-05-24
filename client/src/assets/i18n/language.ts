import { English } from "./en";
import { Lithuanian } from "./lt";

const Language = {
  English,
  Lithuanian
};

type LanguageModel = {
  Category: string;
  LessonType: string;
  Users: string;
  Login: string;
  Register: string;
  Configuration: string;
  Reject: string;
  Approve: string;
  Title: string;
  Description: string;
  Back: string;
  ManageLessons: string;
  Profile: string;
  Password: string;
  Username: string;
  Role: string;
  Language: string;
  English: string;
  Lithuanian: string;
  SearchLessonByTitle: string;
  Search: string;
  NoData: string;
  MainPage: string;
  AboutPage: string;
  FooterNotice: string; 
  Logout: string;
  Categories: string;
  Administrator: string;
  Student: string;
  Teacher: string;
  Author: string;
  Lesson: string;
  Badge: string;
  Update: string;
  Create: string;
  Delete: string;
  UsernameIsRequired: string;
  PasswordIsRequired: string;
  SuccessfullyLoggedInAs: string;
  Add: string;
  Edit: string;
  Approved: string;
  Waiting: string;
  Published: string;
  Rejected: string;
  Created: string;
  Publish: string;
  CategoryTitle: string;
  LessonTypeTitle: string;
  CreateCategory: string;
  UpdateCategory: string;
  CrateLessonType: string;
  UpdateLessonType: string;
  SearchByTitle: string;
  TypeQuantity: string;
  Question: string;
  AddQuestion: string;
  Answer: string;
  State: string;
  Active: string;
  Completed: string;
  Points: string;
  Choose: string;
};

export { Language, LanguageModel };
