import { useAuthentication } from "aws-amplify-react";

const Cognito = () => {
   const { signIn } = useAuthentication();

   const handleSubmit = (event) => {
      event.preventDefault();

      const form = event.target;
      const username = form.elements.username.value;
      const password = form.elements.password.value;

      signIn(username, password);
   };

   return (
      <div>
         <h1>Login</h1>
         <form onSubmit={handleSubmit}>
            <label htmlFor="username">Username:</label>
            <input type="text" id="username" name="username" />
            <br />
            <label htmlFor="password">Password:</label>
            <input type="password" id="password" name="password" />
            <br />
            <button type="submit">Sign In</button>
         </form>
      </div>
   );
};

export default Cognito;
