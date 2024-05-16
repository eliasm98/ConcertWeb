import { useState } from "react";
import Header from "../components/Header/Header";
import axios from "axios";
import { Navigate } from "react-router-dom";

function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(localStorage.getItem('jwtToken')!==null);

  const login = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.get(`http://localhost:5016/sign-in?emailOrUsername=${username}&Password=${password}`);
      const token = response.data.token;
      setIsLoggedIn(true);
      localStorage.setItem('jwtToken', token);
    } catch (error) {
      console.error("An error occurred: ", error);
    }
  }

  return (
    <>
      <Header />
      <div className="container mt-5">
        <form className="w-50 mx-auto">
          <div className="form-group">
            <label htmlFor="username">Username</label>
            <input
              type="text"
              className="form-control"
              id="username"
              name="username"
              value={username}
              onChange={e => setUsername(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">Password</label>
            <input
              type="password"
              className="form-control"
              id="password"
              name="password"
              value={password}
              onChange={e => setPassword(e.target.value)}
            />
          </div>
          <button type="submit" className="btn btn-primary" onClick={(e) => login(e)}>Login</button>
          {isLoggedIn && <Navigate to="/" />}
        </form>
      </div>
    </>
  );
}

export default Login;