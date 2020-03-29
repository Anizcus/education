import Axios from "axios";

const Service = Axios.create({
   baseURL: "http://localhost:5000/",
   headers: {
      Authorization: `Bearer ${localStorage.getItem("session") || "Unknown"}`
   }
});

// Service.interceptors.response.use(
//    response => response.data
// );

export { Service };
