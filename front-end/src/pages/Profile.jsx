import axios from "axios";
import Header from "../components/Header/Header";
import { useState, useEffect } from "react";

function Profile() {
  const [concerts, setConcerts] = useState(null);
  const token = localStorage.getItem('jwtToken');

  useEffect(() => {
    const getBookedEvents = async () => {
      try {
        const headers = {
          Authorization: `Bearer ${token}`
        };
        const response = await axios.get('https://localhost:7121/api/EventsAPI/GetUserBookings', {headers});
        const data = response.data;
        console.log(data);
        const elements = data.map((item, index) => {
          const date = new Date(item.date);
          const year = date.getFullYear();
          const month = String(date.getMonth() + 1).padStart(2, "0");
          const day = String(date.getDate()).padStart(2, "0");
          const formattedDate = `${year}-${month}-${day}`;
          return (
            <div className="container2" key={index}>
              <h1>{item.artistName}</h1>
              <h4>{item.genre}</h4>
              <h4>TicketNB: {item.bookingID}</h4>
              <h2>{formattedDate} at {item.venueName}</h2>
            </div>
          );
        });
        setConcerts(elements);
      } catch (error) {
        console.error("An error occurred: ", error);
      }
    };

    getBookedEvents();
  }, []);

  return (
    <>
      <Header />
      <div className="container1">
        {concerts}
      </div>
    </>
  );
}

export default Profile;