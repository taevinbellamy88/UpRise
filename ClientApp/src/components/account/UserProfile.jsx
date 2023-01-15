// @flow
import React, { useState, useEffect } from "react";
import { Card, Row, Col } from "react-bootstrap";
import "./profile.css";
import Icon from "../../public/icons8-user-96.png";
import { SESSION_STORAGE_GSI_KEY } from "../../services/serviceHelper.js";

const UserProfile = () => {
   const [data] = useState({
      firstName: "",
      lastName: "",
      avatarUrl: "",
      email: "",
      phone: "",
   });

   useEffect(() => {
      const sessionObj = window.sessionStorage.getItem(SESSION_STORAGE_GSI_KEY);
      console.log(sessionObj);
   }, []);

   const editClicked = (e) => {
      console.log("Send Files That Are Deleted Clicked", e);
      e.preventDefault();
      //data.onEditClick(e);
   };

   return (
      { data } && (
         <>
            <div className="container profile-user-box mt-4">
               <Card className="bg-white">
                  <Card.Body className="m-2">
                     <Row>
                        <Col sm={8}>
                           <Row className="align-items-center">
                              <Col className="col-auto">
                                 <div className="avatar-lg-center">
                                    <img
                                       src={Icon}
                                       style={{ height: "100px" }}
                                       alt=""
                                       className="rounded-circle img-thumbnail"
                                    />
                                 </div>
                              </Col>
                              <Col>
                                 <div>
                                    <h4 className="mt-1 mb-1 text-black">
                                       {data?.firstName} {data?.lastName}
                                    </h4>
                                    <p className="mt-1 mb-1 font-20 text-black">
                                       {" "}
                                       Civil Engineer
                                    </p>
                                    <div className="select-form-profile">
                                       <p className="mt-1 mb-1 text-black">
                                          <strong>Mobile:</strong>
                                          <span className="ms-1">{data?.phone}</span>
                                       </p>

                                       <p className="mt-1 mb-1 text-black">
                                          <strong>Email:</strong>
                                          <span className="ms-0"> {data?.email}</span>
                                       </p>
                                    </div>
                                 </div>
                              </Col>
                           </Row>
                        </Col>

                        <Col sm={4}>
                           <div className="text-align-left mt-sm-0 mt-3 text-sm-end">
                              <button
                                 type="button"
                                 className="bg-light btn userprofile-multiple-button text-black"
                                 onClick={editClicked}
                              >
                                 Edit Profile
                              </button>
                           </div>
                        </Col>
                     </Row>
                  </Card.Body>
               </Card>
            </div>
         </>
      )
   );
};

export default UserProfile;
