import Vue from "vue";
import Router from "vue-router";
import LessonCategory from "../views/LessonCategory";
import LessonType from "../views/LessonType";
import LessonList from "../views/LessonList";
import About from "../views/About.vue";
import Login from "../views/Login.vue";
import Register from "../views/Register.vue";
import Lesson from "../views/Lesson.vue";
import Profile from "../views/Profile.vue";
import Users from "../views/Users.vue";
import Configuration from "../views/Configuration.vue";
import Management from "../views/Management.vue";
import Search from "../views/Search.vue";

Vue.use(Router);

const router = new Router({
  routes: [
    {
      path: "/",
      name: "Lesson Category",
      component: LessonCategory
    },
    {
      path: "/lesson/category/:id",
      name: "Lesson Type",
      component: LessonType
    },
    {
      path: "/lesson/type/:id",
      name: "Lesson List",
      component: LessonList
    },
    {
      path: "/lesson/:id",
      name: "Lesson",
      component: Lesson
    },
    {
      path: "/about",
      name: "About",
      component: About
    },
    {
      path: "/user/login",
      name: "Login",
      component: Login
    },
    {
      path: "/user/profile/:id",
      name: "Profile",
      component: Profile
    },
    {
      path: "/user/register",
      name: "Register",
      component: Register
    },
    {
      path: "/users",
      name: "Users",
      component: Users
    },
    {
      path: "/configuration",
      name: "Configuration",
      component: Configuration
    },
    {
      path: "/management",
      name: "Management",
      component: Management
    },
    {
      path: "/search",
      name: "Search",
      component: Search,
      props: true
    }
  ]
});

export default router;
