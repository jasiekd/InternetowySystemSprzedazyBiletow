import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

export default function SuggestedEvents({
  getPredictedEvents,
  setSwalShown,
  MySwal,
}) {
  const [suggestedEvents, setSuggestedEvents] = useState(null);
  const navigate = useNavigate();
  useEffect(() => {
    getPredictedEvents().then((result) => {
      setSuggestedEvents(result);
    });
  }, []);

  return (
    <div style={{ justifyContent: "center" }}>
      {suggestedEvents ? (
        <div>
          <div className="content-header">
            {
              suggestedEvents.length===0?
              "Brak sugerowanych wydarzeń!"
              :
              "Sugerowane wydarzenia specjalnie dla Ciebie!"
            }
            
          </div>
          <div className="place-content">
            {suggestedEvents.map((val, key) => {
              return (
                <div className="trendy-event" key={key}>
                  <img className="trendy-event-img" src={val.imgURL} alt="" />
                  <div className="trendy-info">
                    <div className="trendy-title">{val.title}</div>
                    <div className="trendy-place">{val.location.name}</div>
                    <div className="trendy-price">od {val.ticketPrice} zł</div>
                    <button
                      className="main-btn"
                      onClick={() => {
                        setSwalShown(false);
                        MySwal.close();
                        navigate("/event", {
                          state: { eventId: val.eventID },
                        });
                      }}
                    >
                      Kup teraz
                    </button>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      ) : null}
    </div>
  );
}
