import React, { useEffect, useState } from "react";
import "../styles/ChooseTicket.css";
import "../styles/SummaryBuy.css";
import QRCode from "react-qr-code";
import { useReactToPrint } from "react-to-print";
import moment from "moment";
import SuggestedEvents from "../components/SuggestedEvents";
import { createPortal } from "react-dom";
import UserEventHistoryController from "../controllers/UserEventHistoryController";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const Ticket = React.forwardRef(function Ticket(props, ref) {
  return (
    <>
      {props.userData ? (
        <table className="summart-table" ref={ref}>
          <thead>
            <tr>
              <th>Kod biletu</th>
              <th></th>
              <th>Dane klienta</th>
              <th>Dane biletu</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>
                <QRCode value={JSON.stringify(props.eventData.ticketID)} />
              </td>
              <td className="ticket-img">
                <img src={props.eventData.event.imgURL} />
                <div>{props.eventData.event.title}</div>
                <div>
                  data:
                  {moment(props.eventData.event.date).format("DD-MM-YYYY")}
                </div>
                <div>
                  godzina:
                  {moment(props.eventData.event.date).format("hh:mm")}
                </div>
                <div>lokalizacja: {props.eventData.event.location.name}</div>
              </td>
              <td>
                <div className="ticket-info">
                  <div>Imie: {props.userData.name}</div>
                  <div>Nazwisko: {props.userData.surname}</div>
                  <div>Email: {props.userData.email}</div>
                  <div>
                    Data urodzenia:
                    {moment(props.userData.dateOfBirth).format("DD-MM-YYYY")}
                  </div>
                </div>
              </td>
              <td className="ticket-cost">
                <div>Ilość biletów: {props.eventData.numberOfTickets}</div>
                <div>Wartość: {props.eventData.cost} PLN</div>
              </td>
            </tr>
          </tbody>
        </table>
      ) : null}
    </>
  );
});
const SuggestedEventsModal = () => {
  const MySwal = withReactContent(Swal);
  const [swalShown, setSwalShown] = useState(false);

  useEffect(() => {
    const showSwal = () => {
      MySwal.fire({
        position: "center",
        showConfirmButton: false,
        width: "auto",
        background: "#e7e7e7",

        didOpen: () => setSwalShown(true),
        didClose: () => setSwalShown(false),
      });
    };
    showSwal();
  }, []);
  return (
    <>
      {swalShown &&
        createPortal(
          <div className="trendy">
            <div>
              <UserEventHistoryController>
                <SuggestedEvents setSwalShown={setSwalShown} MySwal={MySwal} />
              </UserEventHistoryController>
            </div>
          </div>,
          MySwal.getHtmlContainer()
        )}
    </>
  );
};
export default function SummaryBuy({ eventData, getUser }) {
  const ref = React.useRef(null);
  const handlePrint = useReactToPrint({
    content: () => ref.current,
  });
  const [userData, setUserData] = useState();
  useEffect(() => {
    getUser().then((r) => {
      setUserData(r);
    });
  }, [eventData]);
  return (
    <div className="summary">
      <>{SuggestedEventsModal()}</>
      {eventData && eventData.event ? (
        <Ticket ref={ref} eventData={eventData} userData={userData} />
      ) : null}

      <button className="main-btn" onClick={handlePrint}>
        Zapisz do pliku
      </button>
    </div>
  );
}
