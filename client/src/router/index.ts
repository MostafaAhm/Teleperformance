import { createRouter, createWebHashHistory, RouteRecordRaw } from "vue-router";
import RegisterForm from "../components/RegisterForm.vue";
import LoginForm from "../components/LoginForm.vue";

const DashboardForm = () => import("../components/DashboardForm.vue");
const EmployeeForm = () => import("../components/EmployeeForm.vue");
const routes = [
  {
    path: "/",
    alias: ["/login"],
    name: "LoginForm",
    component: LoginForm,
  },
  {
    path: "/register",
    name: "Register",
    component: RegisterForm,
  },
  {
    path: "/Dashboead",
    name: "dashboard",
    component: DashboardForm,
  },
  {
    path: "/Employee/:id",
    name: "employee",
    component: EmployeeForm,
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});
router.beforeEach((to, from, next) => {
  const publicPages = ["/login", "/register"];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("token");
  if (authRequired && !loggedIn) {
    next("/login");
  } else {
    next();
  }
  if (authRequired) {
    next("/login");
  } else {
    next();
  }
});

export default router;
