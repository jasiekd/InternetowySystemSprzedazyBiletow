import React from "react";
import '../styles/UserProfile.css';
import Header from '../components/Header';
import Footer from '../components/Footer';
import ProfileTabs from "../components/ProfileTabs";
import LoginController from "../controllers/Login";
function UserProfile(){

   
    return(
        <div className="App">
            




            <Header/>
            <main className='content'>                
                    <div className="content-data">
                        <LoginController>
                            <ProfileTabs/>
                        </LoginController>
                            
                    </div>
                    
                </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>
        
    )
}
export default UserProfile;