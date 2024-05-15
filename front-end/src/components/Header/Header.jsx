import { Link } from "react-router-dom";
import './Header.css';
import axios from "axios";
import { useEffect, useState } from "react";

function Header() {
  const [account, setAccount] = useState(null);
  const token = localStorage.getItem('jwtToken');


  useEffect(() => {
    const fetchUsername = async () => {
      try {
        const headers = {
          Authorization: `Bearer ${token}`
        };
        const response = await axios.get('http://localhost:5016/get-user', {headers});
        console.log(response);
        setAccount(<Link to="/">{response.data.user.userName}</Link>);
        return response;
      } catch (error) {
        console.error("Error fetching username:", error);
      }
    }
  
    if (token) {
      fetchUsername();
    } else {
      setAccount(
        <>
          <Link to="/login">Login</Link>
          <Link to="/signup">Signup</Link>
        </>
      );
    }
  }, [token])
 

  return (
    <div className="header">
      <nav>
        <div className="home">
          <Link to="/">Home</Link>
        </div>
        <div className="account">
          {account}
        </div>
      </nav>
    </div>
  );
}

export default Header;