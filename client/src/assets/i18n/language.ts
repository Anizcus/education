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
};

export { Language, LanguageModel };
