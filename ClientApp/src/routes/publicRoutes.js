import { lazy } from "react";

const Landing = lazy(() => import("../landing/index.js"));
const Login = lazy(() => import("../components/account/Login.jsx"));
const UserProfile = lazy(() => import("../components/account/UserProfile.jsx"));
const Register = lazy(() => import("../components/account/Register.jsx"));
const UserForm = lazy(() => import("../pages/user/UserForm"));
const Users = lazy(() => import("../pages/user/Users"));
const routes = [
  {
    path: "/",
    name: "Landing",
    exact: true,
    element: Landing,
    roles: [],
    isAnonymous: true,
  },
  {
    path: "/register",
    name: "Register",
    exact: true,
    element: Register,
    roles: [],
    isAnonymous: true,
  },
  {
    path: "/login",
    name: "Login",
    exact: true,
    element: Login,
    roles: [],
    isAnonymous: true,
  },
  {
    path: "/user/profile",
    name: "UserProfile",
    exact: true,
    element: UserProfile,
    roles: [],
    isAnonymous: true,
  },
  {
    path: "/userForm",
    name: "userForm",
    exact: true,
    element: UserForm,
    roles: [],
    isAnonymous: true,
  },
  {
    path: "/users",
    name: "Users",
    exact: true,
    element: Users,
    roles: [],
    isAnonymous: true,
  },
];

var allRoutes = [...routes];

export default allRoutes;
