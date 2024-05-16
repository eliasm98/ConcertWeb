import { useState } from "react";
import Header from "../components/Header/Header";
import axios from "axios";

function Signup() {
  const [email, setEmail] = useState('');
  const [userName, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [selectedRole, setSelectedRole] = useState('User');
  const [dateOfBirth, setDateOfBirth] = useState(null);

  const signUp = async () => {
    try {
      const request = {
        email,
        userName,
        password,
        firstName,
        lastName,
        role: selectedRole,
        dateOfBirth
      }
      console.log(request);
      const response = await axios.post('http://localhost:5016/sign-up', request);
      console.log(response);
    } catch (error) {
      console.error("An error occurred: ", error);
    }
  }

  return (
    <>
      <Header />
      <div className="container mt-5">
      <form>
        <div className="form-group">
          <label htmlFor="email">Email</label>
          <input
            type="email"
            className="form-control"
            id="email"
            placeholder="Email"
            name="email"
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label htmlFor="userName">Username</label>
          <input
            type="text"
            className="form-control"
            id="userName"
            placeholder="Username"
            name="userName"
            value={userName}
            onChange={e => setUsername(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            className="form-control"
            id="password"
            placeholder="Password"
            name="password"
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label htmlFor="firstName">First Name</label>
          <input
            type="text"
            className="form-control"
            id="firstName"
            placeholder="First Name"
            name="firstName"
            value={firstName}
            onChange={e => setFirstName(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label htmlFor="lastName">Last Name</label>
          <input
            type="text"
            className="form-control"
            id="lastName"
            placeholder="Last Name"
            name="lastName"
            value={lastName}
            onChange={e => setLastName(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label>Role</label>
          <div className="form-check">
            <input
              className="form-check-input"
              type="radio"
              name="role"
              id="admin"
              value="Admin"
              checked={selectedRole === 'Admin'}
              onChange={e => setSelectedRole(e.target.value)}
            />
            <label className="form-check-label" htmlFor="admin">
              Admin
            </label>
          </div>
          <div className="form-check">
            <input
              className="form-check-input"
              type="radio"
              name="role"
              id="user"
              value="User"
              checked={selectedRole === 'User'}
              onChange={e => setSelectedRole(e.target.value)}
            />
            <label className="form-check-label" htmlFor="user">
              User
            </label>
          </div>
        </div>
        <div className="form-group">
          <label htmlFor="dateOfBirth">Date Of Birth</label>
          <input
            type="date"
            className="form-control"
            id="dateOfBirth"
            name="dateOfBirth"
            value={dateOfBirth}
            onChange={e => setDateOfBirth(e.target.value)}
          />
        </div>
        <button type="submit" onClick={() => signUp()} className="btn btn-primary">
          Sign Up
        </button>
      </form>
    </div>
    </>
  );
}

export default Signup;