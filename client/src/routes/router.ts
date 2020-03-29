import Vue from "vue";
import Router from "vue-router";
import Home from "../views/Home.vue";
import About from "../views/About.vue";
import Login from "../views/Login.vue";

Vue.use(Router);

const router = new Router({
  routes: [
    {
      path: "/",
      name: "Home",
      component: Home
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
    }
  ]
});

export default router;
