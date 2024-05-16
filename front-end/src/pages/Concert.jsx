import axios from "axios";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import Header from "../components/Header/Header";
import { jwtDecode } from 'jwt-decode';
import { Container, Row, Col, Button } from 'react-bootstrap';

function Concert() {
  const [artistName, setArtistName] = useState('');
  const [genre, setGenre] = useState('');
  const [venueName, setVenueName] = useState('');
  const [duration, setDuration] = useState(0);
  const [ticketAmount, setTicketAmount] = useState(0);
  const [price, setPrice] = useState(0);
  const [date, setDate] = useState('');
  const [role, setRole] = useState('');
  const [loading, setLoading] = useState(true);
  const [wallet, setWallet] = useState(0);

  const token = localStorage.getItem('jwtToken');
  const location = useLocation();
  const navigate = useNavigate();
  const queryParams = new URLSearchParams(location.search);
  const id = queryParams.get('id');

  useEffect(() => {
    if (token) {
      const decodedToken = jwtDecode(token);
      if (decodedToken) {
        setRole(decodedToken.role);
      }
    }
    getConcertById();
    fetchWallet();
  }, [token, id]);

  const getConcertById = async () => {
    try {
      const response = await axios.get(`https://localhost:7121/api/EventsAPI/GetConcertById/${id}`);
      const data = response.data;
      setArtistName(data.artistName);
      setGenre(data.genre);
      setVenueName(data.venueName);
      setDuration(data.duration);
      setTicketAmount(data.ticketAmount);
      setPrice(data.price);
      const dateObj = new Date(data.date);
      const year = dateObj.getFullYear();
      const month = String(dateObj.getMonth() + 1).padStart(2, "0");
      const day = String(dateObj.getDate()).padStart(2, "0");
      const formattedDate = `${year}-${month}-${day}`;
      setDate(formattedDate);
      setLoading(false);
    } catch (error) {
      console.error("An error occurred: ", error);
    }
  };

  const fetchWallet = async () => {
    try {
      const headers = {
        Authorization: `Bearer ${token}`
      };
      const response = await axios.get('http://localhost:5016/get-user', {headers});
      setWallet(response.data.user.wallet);
      return response;
    } catch (error) {
      console.error("Error fetching wallet:", error);
    }
  }

  const postConcert = async (e) => {
    e.preventDefault();
    try {
      const request = {
        concertID: id,
        artistName,
        genre,
        venueName,
        duration,
        ticketAmount,
        price,
        date
      };
      const headers = {
        Authorization: `Bearer ${token}`
      };
      const response = await axios.put('https://localhost:7121/api/EventsAPI/UpdateConcert', request, { headers });
      navigate('/');
    } catch (error) {
      console.error("An error occurred: ", error);
    }
  };

  const createBooking = async () => {
    try {
      const headers = {
        Authorization: `Bearer ${token}`
      };
      await axios.post('https://localhost:7121/api/EventsAPI/CreateBooking', {
        concertID: id
      }, {headers});  
      navigate('/profile')
    } catch (error) {
      console.error('An error occurred:', error);
    }
  };

  const renderBody = () => {
    if (loading) {
      return <h5>Page is loading</h5>;
    }

    if (role === 'Admin') {
      return (
        <div className="container">
          <form onSubmit={postConcert}>
            <div className="form-group">
              <label>Artist Name</label>
              <input
                type="text"
                className="form-control"
                value={artistName}
                onChange={e => setArtistName(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label>Genre</label>
              <input
                type="text"
                className="form-control"
                value={genre}
                onChange={e => setGenre(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label>Venue Name</label>
              <input
                type="text"
                className="form-control"
                value={venueName}
                onChange={e => setVenueName(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label>Duration</label>
              <input
                type="number"
                className="form-control"
                value={duration}
                onChange={e => setDuration(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label>Ticket Amount</label>
              <input
                type="number"
                className="form-control"
                value={ticketAmount}
                onChange={e => setTicketAmount(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label>Price</label>
              <input
                type="number"
                className="form-control"
                value={price}
                onChange={e => setPrice(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label>Date</label>
              <input
                type="date"
                className="form-control"
                value={date}
                onChange={e => setDate(e.target.value)}
              />
            </div>
            <button type="submit" className="btn btn-primary" onClick={e => postConcert(e)}>Update Concert</button>
          </form>
        </div>
      );
    } else if (role === "User") {
      return (
        <>
          <p>Wallet: {wallet}$</p>
          <Container>
            <Row>
              <Col>
                <h1>Artist: {artistName}</h1>
                <h3>Genre: {genre}</h3>
                <h2>Venue: {venueName}</h2>
                <h3>Duration: {duration}</h3>
                <h3>Ticket Amount: {ticketAmount}</h3>
                <h2>Price: {price}$</h2>
                <h3>Date: {date}</h3>
              </Col>
            </Row>
            <Button onClick={createBooking}>Book Concert</Button>
          </Container>
        </>
      );
    }
    return (
      <Container>
        <Row>
          <Col>
            <h1>Artist: {artistName}</h1>
            <h3>Genre: {genre}</h3>
            <h2>Venue: {venueName}</h2>
            <h3>Duration: {duration}</h3>
            <h3>Ticket Amount: {ticketAmount}</h3>
            <h2>Price: {price}$</h2>
            <h3>Date: {date}</h3>
          </Col>
        </Row>
      </Container>
    );
  };

  return (
    <>
      <Header />
      {renderBody()}
    </>
  );
}

export default Concert;