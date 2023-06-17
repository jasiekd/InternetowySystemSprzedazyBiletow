import axios from "axios";
import { HostName } from "./HostName";

async function refreshAccessToken() {
    try {
      const response = await axios.post(HostName+"/api/Token/refresh", {
        accessToken: localStorage.accessToken,
        refreshToken: localStorage.refreshToken
      });
      localStorage.accessToken = response.data.accessToken;
      localStorage.refreshToken = response.data.refreshToken;
    } catch (err) {
      console.error(err);
      throw err;
    }
  }
  
  // Axios interceptor to handle expired tokens
  axios.interceptors.response.use(
    (response) => response,
    (error) => {
      const originalRequest = error.config;
  
      if ((error.response.status === 401||error.response.status === 500) && !originalRequest._retry) {
        originalRequest._retry = true;
        return refreshAccessToken().then(() => {
          originalRequest.headers.Authorization = `Bearer ${localStorage.accessToken}`;
          return axios(originalRequest);
        });
      }
  
      return Promise.reject(error);
    }
  );