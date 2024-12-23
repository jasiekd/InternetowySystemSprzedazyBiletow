import * as React from "react";
import Footer from "../components/Footer";
import "../styles/MainStyle.css";
import { useLocation, useNavigate } from "react-router-dom";
import Header from "../components/Header";
import "../styles/Event.css";
import EventComponent from "../components/EventComponent.js";
import LocationComponent from "../components/LocationComponent.js";
import moment from "moment";
import CommentComponent from "../components/CommentComponent";
import CommentController from "../controllers/CommentController";
export default function Event({ getEvent, canBuyTicket }) {
  const location = useLocation();
  const navigate = useNavigate();

  const [canBuy, setCanBuy] = React.useState(false);
  const [ready, setReady] = React.useState(false);
  const [eventInfos, setEventInfos] = React.useState();
  React.useEffect(() => {
    if (location.state === null) {
      navigate("/home");
    } else {
      setReady(true);
      getEvent(location.state.eventId).then((r) => {
        setEventInfos(r);
      });
      canBuyTicket(location.state.eventId).then((r) => {
        setCanBuy(r);
      });
    }
  }, []);

  return (
    <>
      <Header />
      <main className="content">
        {eventInfos ? (
          <>
            <EventComponent
              eventImg={eventInfos.imgURL}
              eventTitle={eventInfos.title}
              eventDate={eventInfos?.date?.$d}
              eventPlace={eventInfos.location?.name}
              eventText={eventInfos.description}
              eventData={eventInfos}
              availableSeats={eventInfos.availableSeats}
              seats={eventInfos.seats}
              eventPrice={eventInfos.ticketPrice}
              canBuy={canBuy}
            />
            <LocationComponent
              localImg={eventInfos.location?.imgURL}
              localText={eventInfos.location?.description}
              location={eventInfos.location}
            />
            <CommentController>
              <CommentComponent eventID={eventInfos.eventID} />
            </CommentController>
          </>
        ) : null}
      </main>
      <div className="App-footer">
        <Footer />
      </div>
    </>
  );
}
