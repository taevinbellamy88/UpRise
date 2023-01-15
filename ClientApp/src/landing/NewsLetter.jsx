import React from "react";
import PropTypes from "prop-types";
import MainHero from "../components/MainHero";
// import newsletterService from "../../services/newsletterService";
// import { Formik, Form, Field, ErrorMessage } from "formik";
// import toastr from "toastr";
// import swal from "@sweetalert/with-react";

const NewsSubMain = () => {
   //    const [formData] = useState({
   //       email: "",
   //    });

   //    const handleSubmit = (values) => {
   //       newsletterService
   //          .addSubscription(values)
   //          .then(onSubmitSuccess)
   //          .catch(onSubmitError);
   //    };

   //    const onSubmitSuccess = (values) => {
   //       console.log("values", values.email);
   //       toastr.success("Subscribed!");
   //       newsletterService.sendNewsletterConfirmation(values);
   //    };

   //    const onSubmitError = () => {
   //       swal({
   //          buttons: {
   //             cancel: "Ok",
   //          },
   //          title: "Please enter a valid email!",
   //          icon: "error",
   //       });
   //    }; const onSubmitSuccess = (values) => {
   //       console.log("values", values.email);
   //       toastr.success("Subscribed!");
   //       newsletterService.sendNewsletterConfirmation(values);
   //    };

   //    const onSubmitError = () => {
   //       swal({
   //          buttons: {
   //             cancel: "Ok",
   //          },
   //          title: "Please enter a valid email!",
   //          icon: "error",
   //       });
   //    };

   return (
      <>
         <MainHero></MainHero>
      </>
   );
};

NewsSubMain.propTypes = {
   id: PropTypes.string.isRequired,
   isLightBg: PropTypes.bool.isRequired,
   isLightText: PropTypes.bool.isRequired,
   topLine: PropTypes.string.isRequired,
   headline: PropTypes.string.isRequired,
   description: PropTypes.string.isRequired,
   buttonLabel: PropTypes.string,
   isImgStart: PropTypes.bool.isRequired,
   //    img: PropTypes.string.isRequired,
   alt: PropTypes.string.isRequired,
   isPrimary: PropTypes.bool.isRequired,
   isDarkText: PropTypes.bool.isRequired,
   isForm: PropTypes.bool,
};

export default NewsSubMain;
