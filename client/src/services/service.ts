import Axios from "axios";
import Store from "@/stores/store";

const Service = Axios.create({
  baseURL: "http://localhost:5000/",
  headers: {
    Authorization: `Bearer ${localStorage.getItem("session") || "Unknown"}`
  }
});

Service.interceptors.response.use(
  response => {
    return response.data;
  },
  error => {
    if (error.request.status == 401) {
      Store.dispatch("user/logout");
    } else {
      return Promise.reject(error);
    }
  }
);

export { Service };
