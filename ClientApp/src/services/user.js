import axios from "axios";
import {
  API_HOST_PREFIX,
  onGlobalError,
  onGlobalSuccess,
} from "./serviceHelper";

const userUrl = `${API_HOST_PREFIX}/api/users/`;

const addUser = (payload) => {
  const config = {
    method: "POST",
    url: `${userUrl}`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateUser = (payload) => {
  const config = {
    method: "POST",
    url: `${userUrl}`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getUserById = (userId) => {
  const config = {
    method: "POST",
    url: `${userUrl}/${userId}`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getAllUsers = () => {
  const config = {
    method: "POST",
    url: `${userUrl}`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const deleteUser = (userId) => {
  const config = {
    method: "POST",
    url: `${userUrl}/${userId}`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const UserService = {
  addUser,
  updateUser,
  deleteUser,
  getUserById,
  getAllUsers,
};

export default UserService;
