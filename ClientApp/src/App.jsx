/* eslint-disable react-hooks/exhaustive-deps */
import React, { Suspense, useState, useEffect, useCallback } from "react";
import { Routes, Route, useLocation } from "react-router-dom";
import DefaultLayout from "./layouts/Default";
//import UserService from "./components/services/userService";
import "./utils/toastr.css";
import {
   authProtectedFlattenRoutes,
   publicProtectedFlattenRoutes,
} from "../src/routes";

const DEFAULT_USER = {
   id: 9,
   roles: ["Admin"],
   email: "testUser@gmail.com",
   isLoggedIn: true,
};
const loading = () => <div className="">loading....</div>;

export default function App(props) {
   const { pathname } = useLocation();
   const { state } = useLocation();
   let [currentUser, setCurrentUser] = useState(() => {
      return DEFAULT_USER;
   });

   useEffect(() => {
      if (state && state.type === "LOGIN_SUCCESS") {
         setCurrentUser({
            id: state.payload.id,
            roles: [...state.payload.roles],
            email: state.payload.name,
            isLoggedIn: true,
         });
      }
      if (state && state.type === "LOGOUT_SUCCESS") {
         setCurrentUser({
            id: 0,
            roles: [],
            email: "",
            isLoggedIn: false,
         });
      }
   }, [state]);

   // useEffect(() => {
   //    UserService.current().then(onCurrentUserSuccess).catch(onCurrentUserError);
   // }, []);

   // const onCurrentUserSuccess = (response) => {
   //    setCurrentUser({
   //       id: response.item.userId,
   //       roles: [...response.item.roles],
   //       email: response.item.email,
   //       isLoggedIn: true,
   //    });
   // };

   // const onCurrentUserError = (error) => {
   //    console.log(error);
   // };

   const [currentPath, setCurrentPath] = useState({
      isPublic: false,
      isSecured: false,
      isUnknown: false,
   });

   const getRouteMapper = useCallback(
      (user) => (routeData) =>
         (
            <Route
               key={routeData.path}
               path={routeData.path}
               exact={routeData.exact}
               name={routeData.name}
               element={<routeData.element currentUser={user} />}
            />
         ),
      []
   );

   const getMappedRoutes = useCallback(
      (arrOfRouteData, user) => {
         let theseRoutes = arrOfRouteData.map(getRouteMapper(user));
         return theseRoutes;
      },
      [getRouteMapper]
   );

   const currentPathCheck = (pp) => {
      let ppPath = pp.path.split("/").filter((el) => el !== "");
      let pathNameCheck = pathname.split("/").filter((el) => el !== "");
      let result = false;
      if (ppPath.length === pathNameCheck.length) {
         if (pathNameCheck.length === 0) {
            result = true;
         } else {
            for (let a = 0; a < pathNameCheck.length; a++) {
               if (pathNameCheck[a] !== ppPath[a]) {
                  if (
                     ppPath[a].startsWith(":") &&
                     pathNameCheck[a].match(/^[0-9]+$/)
                  ) {
                     result = true;
                  } else {
                     return false;
                  }
               } else {
                  result = true;
               }
            }
         }
      }
      return result;
   };

   // ensure that currentPath.path is set to true, but only if it is false AND it should be true
   useEffect(() => {
      if (publicProtectedFlattenRoutes.some((pp) => currentPathCheck(pp))) {
         if (!currentPath.isPublic) {
            setCurrentPath(() => {
               return { isSecured: false, isPublic: true };
            });
         }
      } else if (authProtectedFlattenRoutes.some((pp) => currentPathCheck(pp))) {
         if (!currentPath.isSecured) {
            setCurrentPath(() => {
               return { isPublic: false, isSecured: true };
            });
         }
      } else if (!currentPath.isUnknown) {
         setCurrentPath(() => {
            return { isUnknown: true };
         });
      }
   }, [pathname, currentPath]);

   const generateDynamicRoutes = (currentUser) => {
      let routes = authProtectedFlattenRoutes.filter((route) => {
         if (route.roles?.length === 0) {
            return true; //all any loggedIn user to see routes that have empty roles
         }
         return route.roles?.some((role) => currentUser.roles.includes(role));
      });

      return getMappedRoutes(routes, currentUser);
   };

   const getLast = (arr) => {
      return [arr[arr.length - 1]];
   };

   console.log("render", {
      pathname,
      currentUser,
      currentPath: JSON.stringify(currentPath),
   });
   return (
      <div>
         <Suspense fallback={loading}>
            {/* if the path is public we do not care about the current User  */}
            {currentPath.isPublic && (
               <DefaultLayout {...props} user={currentUser}>
                  <Routes>
                     {getMappedRoutes(publicProtectedFlattenRoutes, currentUser)}
                  </Routes>
               </DefaultLayout>
            )}

            {/* if the user is logged in and attempting to go to an KNOWN page, that is is also secure/not public  */}
            {currentUser.isLoggedIn &&
               !currentPath.isPublic &&
               !currentPath.isUnknown && (
                  <DefaultLayout {...props} user={currentUser}>
                     <Routes>{generateDynamicRoutes(currentUser)}</Routes>
                  </DefaultLayout>
               )}

            {/* we do not know this url , and so the user status does not matter */}
            {currentPath.isUnknown && (
               <DefaultLayout {...props}>
                  <Routes>
                     {getMappedRoutes(
                        getLast(publicProtectedFlattenRoutes),
                        currentUser
                     )}
                  </Routes>
               </DefaultLayout>
            )}
         </Suspense>
      </div>
   );
}
