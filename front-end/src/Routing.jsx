import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from './pages/Home';
import Signup from './pages/Signup';
import Login from './pages/Login';
import Profile from "./pages/Profile";
import AddConcert from "./pages/AddConcert";
import Concert from "./pages/Concert";

function Routing() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="/login" element={<Login />}/>
        <Route path="/signup" element={<Signup />}/>
        <Route path="/profile" element={<Profile />}/>
        <Route path="/add-concert" element={<AddConcert />}/>
        <Route path="/concert" element={<Concert />}/>
      </Routes>
    </Router>
  );
}

export default Routing;