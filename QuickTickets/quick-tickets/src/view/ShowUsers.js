import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import "../styles/OrganisersApproval.css";
import Header from '../components/Header';
import "../styles/DropDownMenu.css";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import LoginController from "../controllers/Login";
import { useEffect, useState } from "react";
import moment from 'moment/moment';


function Users({GetListOfUsers}){
    
    const [users,setUsers] = useState([]);
    const [pageIndex,setpageIndex] = useState(1);

        useEffect( () => {
                GetListOfUsers(pageIndex,3).then((result)=>{
                    setUsers(result);
                });

        },[pageIndex]);


    return(
        <div className='searchList'>                                   
         
                <div className='title'>Użytkownicy strony</div>
                {       users!== undefined &&
                        users.value?.users?.map((user,index)=>(

                                <div className='organisers-list' style={{boxShadow:" 0 10px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)"}} key={index}>
                                    <div className='organisers-list-row'>
                                        <h3>Dane:</h3>
                                    </div>
                                    <div className='organisers-list-row'>
                                       E-mail: {user.user.email}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Login: {user.user.login}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Imię i nazwisko: {user.user.name + " " + user.user.surname}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Data urodzenia: { moment(user.user.dateOfBirth).format('D.MM.YYYY  h:mm a')}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Data utworzenia konta: { moment(user.dateCreated).format('D.MM.YYYY  h:mm a')}
                                    </div>
                                </div>
                            
                        ))
                   
                }  
                {
                    users!== undefined && users.value?.totalPages>0 &&
                    <Pagination count={users.value.totalPages} size='large' onChange={(e,v)=>{setpageIndex(v)}}/>
                }
        </div>
    );

}

  
export default function ShowUsers(){

    return(
        <div className="App">
            <Header/>
            <main className='content'>
                        <LoginController>
                            <Users />
                        </LoginController>
            </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>

    );
}