import { useState } from "react";
import Header from "../components/Header/Header";
import axios from "axios";
import { Navigate } from "react-router-dom";

function AddConcert() {
  const [artistName, setArtistName] = useState('');
  const [genre, setGenre] = useState('');
  const [venueName, setVenueName] = useState('');
  const [duration, setDuration] = useState(0);
  const [ticketAmount, setTicketAmount] = useState(0);
  const [price, setPrice] = useState(0);
  const [date, setDate] = useState();

  const token = localStorage.getItem('jwtToken');

  const postConcert = async (e) => {
    try {
      const request = {
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
      await axios.post('https://localhost:7121/api/EventsAPI/CreateConcert', request, {headers});
    } catch (error) {
      console.error("An error occurred: ", error);
    }
  }

  return (
    <>
      <Header />
      <div className="container">
        <form>
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
          <button type="submit" className="btn btn-primary" onClick={(e) => postConcert(e)}>Add Concert</button>
        </form>
      </div>
    </>
  );
}

export default AddConcert;