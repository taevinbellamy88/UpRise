import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Row, Col, Button } from "react-bootstrap";
import AccountLayout from "../auth/AccountLayout.jsx";
import { Formik, Form, Field, ErrorMessage } from "formik";
import UserService from "../services/userService.js";
import GSILogin from "../Google.jsx";
//import Cognito from "../Cognito.jsx";

const BottomLink = () => {
   return (
      <Row className="mt-3">
         <Col className="text-center">
            <p className="text-muted">
               Dont have an account?
               <Link to={"/register"} className="text-muted ms-1">
                  <b>Register</b>
               </Link>
            </p>

            <Link to="/forgotpassword">
               <div align="center" className="text-muted ms-1">
                  Forgot Password?
               </div>
            </Link>
         </Col>
      </Row>
   );
};

const Login = () => {
   const navigate = useNavigate();
   const [formData] = useState({
      email: "",
      password: "",
   });

   const handleSubmit = (values) => {
      UserService.login(values).then(onSubmitSuccess).catch(onSubmitError);
      console.log({ values });
   };

   const onSubmitSuccess = (response) => {
      console.log({ response });
      if (response.response.isSuccessful) {
         navigate("/user/profile");
      }
   };

   const onSubmitError = (response) => {
      console.log({ response });
   };

   return (
      <AccountLayout bottomLinks={<BottomLink />}>
         <div className="text-center w-75 m-auto">
            <h2 className="text-dark-50 text-center mt-0 fw-bold">Sign In</h2>
            <p className="text-muted mb-4">
               Enter your email address and password to access admin panel.
            </p>
         </div>
         <Formik
            enableReinitialize={true}
            initialValues={formData}
            onSubmit={handleSubmit}
         >
            <Form>
               <div className="form-group m-2">
                  <label htmlFor="email">Email</label>
                  <Field type="text" name="email" className="form-control" />
                  <ErrorMessage name="email" component="div" className="has-error">
                     {(msg) => <div style={{ color: "red" }}>{msg}</div>}
                  </ErrorMessage>
               </div>
               <div className="form-group m-2">
                  <label htmlFor="password">Password</label>
                  <Field type="password" name="password" className="form-control" />
                  <ErrorMessage name="password" component="div" className="has-error">
                     {(msg) => <div style={{ color: "red" }}>{msg}</div>}
                  </ErrorMessage>
               </div>
               <div className="row justify-content-center">
                  <div className="text-center">
                     <Button type="submit" className="btn btn-primary mx-2 mt-2">
                        Login
                     </Button>
                  </div>
               </div>
            </Form>
         </Formik>
         <div>
            <GSILogin />
            {/* <Cognito /> */}
         </div>
      </AccountLayout>
   );
};

export default Login;
