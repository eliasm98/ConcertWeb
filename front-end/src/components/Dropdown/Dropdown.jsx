import { Link } from "react-router-dom";
import './Dropdown.css';
import { useState } from "react";

function Dropdown({text, role}) {
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setDropdownOpen(!dropdownOpen);
  };

  const closeDropdown = () => {
    setDropdownOpen(false);
  };

  const logOut = () => {
    closeDropdown();
    localStorage.removeItem("jwtToken");
    window.location.reload();
  }

  return (
    <div className="dropdown" onMouseLeave={closeDropdown} onMouseEnter={toggleDropdown}>
      <Link
        to="/"
        className="dropdown-toggle"
      >
        {text}
      </Link>
      {dropdownOpen && (
        <div className="dropdown-menu">
          {role==="Admin" ? <Link to="/add-concert" className="dropdown-item" onClick={closeDropdown}>Add Concert</Link> 
            : <Link to="/profile" className="dropdown-item" onClick={closeDropdown}>Profile</Link>}
          <Link to="/" className="dropdown-item" onClick={logOut}>Logout</Link>
        </div>
      )}
    </div>
  );
}

export default Dropdown