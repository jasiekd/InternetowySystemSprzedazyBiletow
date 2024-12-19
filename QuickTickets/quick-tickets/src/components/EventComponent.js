import { useNavigate } from "react-router-dom";
import people from "../images/people.png";
import moment from "moment/moment";
import Alert from "@mui/material/Alert";
import { checkIsLogged } from "../controllers/Login";
export default function EventComponent({
  eventPrice,
  eventImg,
  eventTitle,
  eventDate,
  eventPlace,
  eventText,
  disableBuy,
  eventData,
  displayPreview,
  seats,
  availableSeats,
  canBuy,
}) {
  const navigate = useNavigate();
  return (
    <div className="event-info">
      <div className="event-title">
        <div className="event-title-header">
          {eventTitle !== "" ? eventTitle : "Nazwa wydarzenia"}
        </div>
        <div>
          {(!isNaN(eventDate) &&
            moment(eventDate.toISOString()).format("DD-MM-YYYY, hh:mm a")) ||
            moment(eventDate).format("DD-MM-YYYY, hh:mm a")}
          {eventPlace !== "Miejsce" ? " " + eventPlace : "Miejsce wydarzenia"}
        </div>
      </div>
      <div className="event-content">
        <div className="event-img-section">
          <img
            className="event-img"
            src={eventImg !== "" ? eventImg : people}
          />
        </div>
        <div className="event-description">
          <div className="event-description-title">Opis wydarzenia</div>

          {displayPreview ? (
            <div className="event-description-text event-preview">
              {eventText !== "" ? eventText : "Opis wydarzenia"}
            </div>
          ) : (
            <div className="event-description-text ">
              {eventText !== "" ? eventText : "Opis wydarzenia"}
            </div>
          )}
          <div style={{ display: "flex", gap: "2rem" }}>
            {availableSeats <= 0 ? (
              <Alert variant="filled" severity="warning">
                Bilety wprzedane
              </Alert>
            ) : checkIsLogged() ? (
              canBuy ? (
                <button
                  className="main-btn"
                  onClick={() =>
                    navigate("/buy-ticket", { state: { event: eventData } })
                  }
                  disabled={disableBuy}
                >
                  Kup teraz za {eventPrice} PLN
                </button>
              ) : (
                <Alert variant="filled" severity="warning">
                  Wydarzenie juz sie odbyło
                </Alert>
              )
            ) : (
              <Alert variant="filled" severity="warning">
                Zaloguj się, aby kupić bilet
              </Alert>
            )}

            <h2> Pozostałe bilety: {availableSeats}</h2>
          </div>
        </div>
      </div>
    </div>
  );
}
