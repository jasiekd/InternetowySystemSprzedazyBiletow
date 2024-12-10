import * as React from "react";
import Footer from "../components/Footer";
import "../styles/MainStyle.css";
import "../styles/OrganisersApproval.css";
import { useNavigate } from "react-router-dom";
import Header from "../components/Header";
import "../styles/DropDownMenu.css";
import "../styles/SearchList.css";
import Pagination from "@mui/material/Pagination";
import OrganiserApplicationController from "../controllers/OrganiserApplicationController";
import { useEffect, useState } from "react";
import Alert from "@mui/material/Alert";
import moment from "moment";

function ShowOrganisers({
  getOrganiserApplication,
  acceptOrganiserApplication,
  cancelOrganiserApplication,
}) {
  const [organisersToAprove, setOrganisersToAprove] = useState();
  const [pageIndex, setpageIndex] = useState(1);

  useEffect(() => {
    getOrganiserApplication(pageIndex, 3).then((result) => {
      setOrganisersToAprove(result);
    });
  }, [pageIndex]);

  const onAcceptOrganiserApplication = (id) => {
    acceptOrganiserApplication(id).then((result) => {
      if (!result) return;
      getOrganiserApplication(pageIndex, 3).then((result) => {
        setOrganisersToAprove(result);
      });
    });
  };

  const onCancelOrganiserApplication = (id) => {
    cancelOrganiserApplication(id).then((result) => {
      if (!result) return;
      getOrganiserApplication(pageIndex, 3).then((result) => {
        setOrganisersToAprove(result);
      });

      // const newOrganisersToAprove=[...organisersToAprove];
      // newOrganisersToAprove.splice(index,1);
      // setOrganisersToAprove(newOrganisersToAprove);
    });
  };

  return (
    <div className="searchList">
      <div className="title">Organizatorzy do zatwierdzenia</div>
      {organisersToAprove !== undefined &&
      organisersToAprove.value?.totalCount > 0 ? (
        organisersToAprove.value.organiserApplications.map((val, index) => {
          return (
            <div className="organisers-list" key={index}>
              <div className="organisers-list-row">
                <h3>Dane:</h3>
              </div>
              <div className="organisers-list-row">
                <div style={{ fontWeight: "700" }}>Imie i nazwisko: </div>
                {val.user.name + " " + val.user.surname}
              </div>
              <div className="organisers-list-row">
                <div style={{ fontWeight: "700" }}>Data urodzenia: </div>{" "}
                {moment(val.user.dateOfBirth).format("DD-MM-YYYY")}
              </div>
              <div className="organisers-list-row">
                <div style={{ fontWeight: "700" }}>Email: </div>
                {val.user.email}
              </div>
              <div className="organisers-list-row">
                <div>
                  <h3>Uzasadnienie:</h3>
                  <p>{val.description}</p>
                </div>
              </div>
              <div className="organisers-list-row">
                <button
                  className="main-btn2"
                  onClick={() => onAcceptOrganiserApplication(val.id, index)}
                >
                  Zatwierdź
                </button>
                <button
                  className="cancel-btn"
                  onClick={() => onCancelOrganiserApplication(val.id, index)}
                >
                  Anuluj
                </button>
              </div>
            </div>
          );
        })
      ) : (
        <Alert variant="filled" severity="info">
          Brak użytkowników do wyświetlenia
        </Alert>
      )}
      {organisersToAprove !== undefined &&
        organisersToAprove.value?.totalCount > 0 && (
          <Pagination
            count={Math.ceil(organisersToAprove.value.totalPages)}
            size="large"
            onChange={(e, v) => setpageIndex(v)}
          />
        )}
    </div>
  );
}

export default function OrganisersApproval() {
  const navigate = useNavigate();

  return (
    <div className="App">
      <Header />
      <main className="content">
        <OrganiserApplicationController>
          <ShowOrganisers />
        </OrganiserApplicationController>
      </main>
      <div className="App-footer">
        <Footer />
      </div>
    </div>
  );
}
