import React, { useState } from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import User from "../../schema/user/user.js";
import "./user.css";

function UserForm() {
  const [formData] = useState({
    email: "",
    userName: "",
    firstName: "",
    lastName: "",
    password: "",
    status: false,
  });

  const handleSubmit = (values) => {
    console.log(values);
  };

  return (
    <div className="container mt-4">
      <div className="row justify-content-center">
        <div className="col-4 shadow ">
          <div className="text-center w-75 m-auto">
            <h2 className="text-dark-50 text-center mt-0 fw-bold">New User</h2>
          </div>
          <Formik
            enableReinitialize={true}
            initialValues={formData}
            onSubmit={handleSubmit}
            validationSchema={User}
          >
            <Form>
              <div className="form-group m-2">
                <lable htmlFor="email" className="form-control-label">
                  Email
                </lable>
                <Field type="email" name="email" className="form-control" />
                <ErrorMessage
                  name="email"
                  component="div"
                  className="has-error"
                />
              </div>
              <div className="form-group m-2">
                <label htmlFor="userName" className="form-control-label">
                  UserName
                </label>
                <Field type="text" name="userName" className="form-control" />
                <ErrorMessage
                  name="userName"
                  component="div"
                  className="has-error"
                />
              </div>
              <div className="form-group m-2">
                <label htmlFor="firstName" className="form-control-label">
                  First Name
                </label>
                <Field type="text" name="firstName" className="form-control" />
                <ErrorMessage
                  name="firstName"
                  component="div"
                  className="has-error"
                />
              </div>
              <div className="form-group m-2">
                <label htmlFor="lastName" className="form-control-label">
                  Last Name
                </label>
                <Field type="text" name="lastName" className="form-control" />
                <ErrorMessage
                  name="lastName"
                  component="div"
                  className="has-error"
                />
              </div>
              <div className="form-group m-2">
                <label htmlFor="password" className="form-control-label">
                  Password
                </label>
                <Field
                  type="password"
                  name="password"
                  className="form-control"
                />
                <ErrorMessage
                  name="password"
                  component="div"
                  className="has-error"
                />
              </div>
              <div className="form-group m-2 text-center">
                <button type="submit" className="btn btn-primary">
                  Submit
                </button>
              </div>
            </Form>
          </Formik>
        </div>
      </div>
    </div>
  );
}

export default UserForm;
