import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { GoogleLogin } from "react-google-login";
import { gapi } from "gapi-script";

function GSILogin() {
   const navigate = useNavigate();

   useEffect(() => {
      function start() {
         gapi.client.init({
            clientId:
               "221415279408-jfrlvj0meit6qgkvajj7a0k5o991p3g2.apps.googleusercontent.com",
            scope: "email",
         });
      }
      gapi.load("client:auth2", start);
   }, []);

   const onSuccess = (response) => {
      console.log("SUCCESS", response);

      if (response.tokenId != null && response.wt.NT != null) {
         window.sessionStorage.setItem("GSI_USER", response.wt.NT);

         navigate("/user/profile");
      }
   };
   const onFailure = (response) => {
      console.log("FAILED", response);
   };
   // const onLogoutSuccess = () => {
   //    console.log("SUCESS LOG OUT");
   // };

   return (
      <div style={{ marginTop: "25px", textAlign: "center" }}>
         <GoogleLogin
            clientId={
               "221415279408-jfrlvj0meit6qgkvajj7a0k5o991p3g2.apps.googleusercontent.com"
            }
            onSuccess={onSuccess}
            onFailure={onFailure}
         />
         <span style={{ marginLeft: "10px" }}></span>
         {/* <GoogleLogout
            clientId={
               "221415279408-jfrlvj0meit6qgkvajj7a0k5o991p3g2.apps.googleusercontent.com"
            }
            onLogoutSuccess={onLogoutSuccess}
         /> */}
      </div>
   );
}

export default GSILogin;
