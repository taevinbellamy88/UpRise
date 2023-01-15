import React, { useState } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { COGNITO_DOMAIN_API } from "../services/serviceHelper.js";

const NavBar = (props) => {
   const [toggleUserBtn] = useState(!props.disableLogin);

   function handleClick() {
      window.location.href = COGNITO_DOMAIN_API;
   }

   //    setToggleLoginBtn(() => {
   //       const toggle = toggleUserBtn;

   //       return toggleLoginBtn;
   //   });

   return (
      <nav
         className="navbar navbar-expand-md navbar-dark bg-dark"
         aria-label="Fourth navbar example"
      >
         <div className="container">
            <button
               className="navbar-toggler"
               type="button"
               data-bs-toggle="collapse"
               data-bs-target="#navbarsExample04"
               aria-controls="navbarsExample04"
               aria-expanded="false"
               aria-label="Toggle navigation"
            >
               <span className="navbar-toggler-icon"></span>
            </button>

            <div className="collapse navbar-collapse" id="navbarsExample04">
               <ul className="navbar-nav me-auto mb-2 mb-md-0">
                  <li className="nav-item">
                     <Link className="nav-link px-2 text-white link-button" to="/">
                        Home
                     </Link>
                  </li>
                  <li className="nav-item">
                     <Button
                        onClick={handleClick}
                        className="nav-link px-2 text-white link-button"
                     >
                        COGNITO
                     </Button>
                  </li>
                  <li className="nav-item">
                     <Link
                        href="#"
                        className="nav-link px-2 text-white link-button"
                        to="/products"
                     >
                        Page 2
                     </Link>
                  </li>
                  <li className="nav-item">
                     <Link
                        href="#"
                        className="nav-link px-2 text-white link-button"
                        to="/testimonies"
                     >
                        Page 3
                     </Link>
                  </li>
               </ul>
               <div className="text-end">
                  {!toggleUserBtn && (
                     <a
                        href="/user/profile"
                        className="align-items-center mb-2 me-2 mb-lg-0 text-white text-decoration-none"
                     >
                        User Profile
                     </a>
                  )}
                  {toggleUserBtn && (
                     <Link
                        type="button"
                        className="btn btn-outline-light me-2"
                        to="/login"
                     >
                        Login
                     </Link>
                  )}
                  {toggleUserBtn && (
                     <Link type="button" className="btn btn-warning" to="/register">
                        Register
                     </Link>
                  )}
               </div>
            </div>
         </div>
      </nav>
   );
};
export default NavBar;
