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
      <form>
        <input type="text" name="username" value={username} onChange={e => {setUsername(e.target.value)}}/>
        <input type="password" name="password" value={password} onChange={e => {setPassword(e.target.value)}}/>
        <button onClick={(e)=>login(e)}>Login</button>
        {isLoggedIn && <Navigate to="/" />}
      </form>
    </>
  );
}

export default Login;