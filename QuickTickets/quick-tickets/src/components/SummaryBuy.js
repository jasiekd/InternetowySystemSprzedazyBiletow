import React, { useEffect, useState } from "react";
import '../styles/ChooseTicket.css';
import '../styles/SummaryBuy.css';
import QRCode from "react-qr-code";
import { useReactToPrint } from "react-to-print";
import moment from "moment";
import LoginController from "../controllers/Login";
class Ticket extends React.PureComponent {
  
  render(){
    return (
      
        this.props.userData?
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
                <QRCode value={JSON.stringify(this.props.eventData.ticketID)} />
              </td>
              <td className="ticket-img">
                <img src={this.props.eventData.event.imgURL} />
                <div>{this.props.eventData.event.title}</div>
                <div>data: {moment(this.props.eventData.event.date).format('DD-MM-YYYY')}</div>
                <div>godzina: {moment(this.props.eventData.event.date).format('hh:mm')}</div>
                <div>lokalizacja: {this.props.eventData.event.location.name}</div>
              </td>
              <td>
                <div className="ticket-info">
                  <div>Imie: {this.props.userData.name}</div>
                  <div>Nazwisko: {this.props.userData.surname}</div>
                  <div>Email: {this.props.userData.email}</div>
                  <div>Data urodzenia: {moment(this.props.userData.dateOfBirth).format('DD-MM-YYYY')}</div>
                </div>
              </td>
              <td className="ticket-cost">
                <div>Ilość biletów: {this.props.eventData.numberOfTickets}</div>
                <div>Wartość: {this.props.eventData.cost} PLN</div>
              </td>
            </tr>
          </tbody>
        </table>
        :
        null
       
      
        
    );
  }  
  
  };
  
  export default function SummaryBuy({eventData,getUser}) {
    const componentRef = React.useRef();
    const handlePrint = useReactToPrint({
      content: () => componentRef.current,
    })
    const [userData,setUserData] = useState();
    useEffect(()=>{
      getUser().then(r=>{
        console.log(r);
        setUserData(r);
        console.log("IN PROGRESS")
        console.log(eventData)
      })
    },[])
    return (
      <div className="summary">
       
          <Ticket 
            ref={componentRef}
            eventData={eventData}  
            userData={userData}
          />
       
        
        <button className="main-btn" onClick={handlePrint}>
          Zapisz do pliku
        </button>
      </div>
    );
  }