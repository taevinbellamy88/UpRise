import axios from "axios";
axios.defaults.withCredentials = true;
// Add a request interceptor
axios.interceptors.request.use(function (config) {
   config.withCredentials = true;
   return config;
});

/**
 * Will unpack the response body from reponse object
 * @param {*} response
 *
 */
const onGlobalSuccess = (response) => {
   /// Should not use if you need access to anything other than the data
   return response.data;
};

const onGlobalError = (err) => {
   return Promise.reject(err);
};

const API_HOST_PREFIX = "http://localhost:50000";
const GOOGLE_CLIENT_ID = "enter your google clientID";
const SESSION_STORAGE_GSI_KEY = "GSI_USER";
const COGNITO_DOMAIN_API = "https://localhost:3000/";
const API_NODE_HOST_PREFIX = process.env.REACT_APP_API_NODE_HOST_PREFIX;

//console.log("API_HOST_PREFIX", API_HOST_PREFIX);

export {
   onGlobalError,
   onGlobalSuccess,
   API_HOST_PREFIX,
   GOOGLE_CLIENT_ID,
   COGNITO_DOMAIN_API,
   SESSION_STORAGE_GSI_KEY,
   API_NODE_HOST_PREFIX,
};
