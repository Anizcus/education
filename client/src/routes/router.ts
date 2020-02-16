import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/home.vue";
import About from "../views/about.vue";
import User from "../views/user/overview.vue";
import Login from "../views/user/login.vue";
import Register from "../views/user/register.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: Home
  },
  {
    path: "/about",
    name: "about",
    component: About
  },
  {
    path: "/user",
    name: "user",
    component: User
  },
  {
    path: "/user/login",
    name: "login",
    component: Login
  },
  {
    path: "/user/register",
    name: "register",
    component: Register
  }
];

const router = new VueRouter({
  routes
});

export default router;
