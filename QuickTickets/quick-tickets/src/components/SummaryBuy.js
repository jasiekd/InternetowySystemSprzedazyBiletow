import React from "react";
import '../styles/ChooseTicket.css';
import '../styles/SummaryBuy.css';
import { GreenInput } from "./GreenInput";
import exampleEvent from "../images/example-event.png";
import EventInfo from "./EventInfo";
import QRCode from "react-qr-code";
import { useReactToPrint } from "react-to-print";

class Ticket extends React.PureComponent {
  render(){
    return (
        <table className="summart-table">
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
                <QRCode value="45457456346" />
              </td>
              <td className="ticket-img">
                <img src={exampleEvent} />
                <div>Lorem ipsum nazwa</div>
                <div>data: 12.12.2023</div>
                <div>godzina: 7:00</div>
                <div>lokalizacja: Kielce</div>
              </td>
              <td>
                <div className="ticket-info">
                  <div>Imie: Jan</div>
                  <div>Nazwisko: Nowak</div>
                  <div>Adres: Kielce ul.Słoneczna 28</div>
                  <div>Email: jannowak@mail.com</div>
                  <div>Tel: 345-346-235</div>
                </div>
              </td>
              <td>
                <div>Ilość biletów: 12</div>
                <div>Wartość: 920 PLN</div>
              </td>
            </tr>
          </tbody>
        </table>
    );
  }  
  
  };
  
  export default function SummaryBuy() {
    const componentRef = React.useRef();
    const handlePrint = useReactToPrint({
      content: () => componentRef.current,
    })

    return (
      <div className="summary">

        <Ticket ref={componentRef}/>
        <button className="main-btn" onClick={handlePrint}>
          Zapisz do pliku
        </button>
      </div>
    );
  }