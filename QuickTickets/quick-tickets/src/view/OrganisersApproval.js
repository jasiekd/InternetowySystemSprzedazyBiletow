import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import "../styles/OrganisersApproval.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/DropDownMenu.css";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';

export default function OrganisersApproval(){
    const navigate = useNavigate();
    

    const OrganisersToAprove =[
        {
            id: "1",
        },
        {
            id: "2",
        },
        {
            id: "3",
        },
        {
            id: "4",
        },
        {
            id: "5",
        },
        {
            id: "6",
        },
        {
            id: "7",
        },
        {
            id: "8",
        }
    ]

    return(
        <div className="App">
        <Header/>
    <main className='content'>

        <div className='searchList'>
            <div className='title'>Organizatorzy do zatwierdzenia</div>
            {
                OrganisersToAprove.map((val,key)=>{
                    return(
                        <div className='organisers-list'>
                            <div className='organisers-list-row'>
                                Jan Nowak
                            </div>
                            <div className='organisers-list-row'>04.01.1995</div>
                            <div className='organisers-list-row'>jan.nowak@wpp.pl</div>
                            <div className='organisers-list-row'>
                                <button className='main-btn'>Zatwierdź</button>
                            </div>
                        </div>
                    )
                })
            }  
            <Pagination count={10} size='large'/>
        </div>
    </main>
    <div className='App-footer'>
        <Footer/>
      </div>
  </div>

    );
}