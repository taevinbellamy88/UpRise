import * as Yup from "yup";

const basicSchema = Yup.object().shape({
  email: Yup.string().email("In valid email").required("Email is Required"),
  userName: Yup.string().min(2).max(50).required("UserName is required"),
  firstName: Yup.string().min(2).max(50).required("first name is required"),
  lastName: Yup.string().min(2).max(50).required("last name is required"),
  password: Yup.string().min(2).max(50).required("password is required"),
  status: Yup.number().required("is Required"),
});

export default basicSchema;
