import React, { useState } from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { Container, Row, Col, Card, Button } from "react-bootstrap";
import icon from "../public/icons8-user-96.png";

const SocialMedia = () => {
   const [posts, setPosts] = useState([]);

   // const onFileSelect = (e, setFieldValue) => {
   //    setFieldValue("image", e.currentTarget.files[0]);
   //    setSelectedImage(URL.createObjectURL(e.currentTarget.files[0]));
   // };

   const [selectedImage, setSelectedImage] = useState(undefined);

   return (
      <Container>
         <Row className="justify-content-center mt-2">
            <Col md={6}>
               <Card>
                  <Card.Header>Create a post</Card.Header>
                  <Card.Body>
                     <Formik
                        initialValues={{ message: "", image: undefined }}
                        onSubmit={(values, { setSubmitting, resetForm }) => {
                           setTimeout(() => {
                              setPosts([...posts, values]);
                              resetForm();
                              setSubmitting(false);
                           }, 400);
                        }}
                     >
                        {({ isSubmitting }) => (
                           <Form>
                              <Field
                                 type="text"
                                 name="message"
                                 placeholder="Enter your message"
                              />
                              <ErrorMessage name="message" component="div" />
                              <div className="form-group mt-2">
                                 <Field
                                    type="file"
                                    name="image"
                                    accept="image/*"
                                    onChange={(e) => {
                                       setSelectedImage(
                                          URL.createObjectURL(e.currentTarget.files[0])
                                       );
                                    }}
                                    className="form-control"
                                 />
                              </div>

                              <Button
                                 className="mt-2"
                                 type="submit"
                                 disabled={isSubmitting}
                              >
                                 Post
                              </Button>
                           </Form>
                        )}
                     </Formik>
                  </Card.Body>
               </Card>
               <div className="container mt-4">
                  <div className="row">
                     <div className="card mx-auto col-12">
                        <div className="card-body">
                           <div className="posts">
                              {posts.map((post, index) => (
                                 <div
                                    className="card mx-auto col-12 m-4"
                                    key={post + index.toString() + "KEY"}
                                 >
                                    <div className="card-body">
                                       <div key={index} className="media my-3">
                                          <img
                                             src={icon}
                                             alt="Profile"
                                             className="align-self-start mr-3 rounded-circle"
                                             style={{ width: "50px", height: "50px" }}
                                          />
                                          <div className="media-body">
                                             <p>{post.message}</p>
                                          </div>
                                          {post.image && (
                                             <img
                                                src={selectedImage}
                                                alt="PostImage"
                                                className="align-self-center mx-auto d-block"
                                                style={{
                                                   width: "50px",
                                                   height: "50px",
                                                }}
                                             />
                                          )}
                                       </div>
                                    </div>
                                 </div>
                              ))}
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
            </Col>
         </Row>
      </Container>
   );
};

export default SocialMedia;
