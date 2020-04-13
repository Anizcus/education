import Vue from "vue";
import Router from "vue-router";
import Home from "../views/Home";
import About from "../views/About.vue";
import Login from "../views/Login.vue";
import Register from "../views/Register.vue";

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
    },
    {
      path: "/user/register",
      name: "Register",
      component: Register
    }
  ]
});

export default router;
