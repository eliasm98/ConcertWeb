import axios from "axios";
import Header from "../components/Header/Header";
import { useEffect, useState } from "react";

function Home() {
  const [concerts, setConcerts] = useState([]);
  const [search, setSearch] = useState('');

  useEffect(() => {
    const getConcerts = async () => {
      try {
        const response = await axios.get('https://localhost:7121/api/EventsAPI/GetAllConcerts');
        const data = response.data;
        const elements = data.map((item, index) => {
          const date = new Date(item.date);
          const year = date.getFullYear();
          const month = String(date.getMonth() + 1).padStart(2, "0");
          const day = String(date.getDate()).padStart(2, "0");
          const formattedDate = `${year}-${month}-${day}`;
          return (
            <div className="container2" key={index}>
              <a href={`./concert?id=${item.concertID}`}><h1>{item.artistName}</h1></a>
              <h4>{item.genre}</h4>
              <h3>{item.price}$</h3>
              <h2>{formattedDate} at {item.venueName}</h2>
            </div>
          );
        });
        setConcerts(elements);
      } catch (error) {
        console.error("An error occurred: ", error);
      }
    }

    getConcerts();
  }, []);

  const searchConcerts = async () => {
    try {
      const response1 = await axios.get(`https://localhost:7121/api/EventsAPI/GetConcertByArtist/${search}`);
      const response2 = await axios.get(`https://localhost:7121/api/EventsAPI/GetConcertByVenue/${search}`);
      let data = []
      if(response1.data.length!=0) {data.push(response1.data);}
      if(response2.data.length!=0) {data.push(response2.data);}
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
            <h3>{item.price}$</h3>
            <h2>{formattedDate} at {item.venueName}</h2>
          </div>
        );
      });
      setConcerts(elements);
    } catch (error) {
      console.error("An error occurred: ", error);
    }
  }
  

  return (
    <>
      <Header />
      <div className="search-container row justify-content-center">
        <div className="col-md-4 text-center">
          <input type="text" className="form-control" placeholder="Search..." value={search} onChange={(e) => setSearch(e.target.value)} />
        </div>
        <div className="col-md-1 text-center">
          <button className="btn btn-primary" onClick={searchConcerts}>Search</button>
        </div>
      </div>
      <div className="container1">
        {concerts}
      </div>
    </>
  );
}

export default Home;