import { useNavigate } from "react-router-dom";
import { GreenInput } from "../components/GreenInput";
import { useState,useEffect } from "react";
import Header from "../components/Header";
import Footer from "../components/Footer";
import { checkIsLogged } from "../controllers/Login";
function AddAdminForm(){
    const navigate = useNavigate();

    const [loginRegVal,setLoginRegVal] = useState("");
    const [passwordRegVal,setPasswordRegVal] = useState("");
    const [emailRegVal,setEmailRegVal] = useState("");
    const [nameRegVal, setNameRegVal] = useState("");
    const [surnameRegVal,setSurnameRegVal] = useState("");
    const [dateOfBirthRegVal,setDateOfBirdthRegVal] = useState();

    const [errorStatus, setErrorStatus] = useState(false);
    const [errorText, setErrorText] = useState("");

    const onChangeLoginVal = (val) => {
        setLoginRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangePasswordVal = (val) => {
        setPasswordRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangeEmailVal = (val) => {
        setEmailRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangeNameVal = (val) => {
        setNameRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangeSurnameVal = (val) => {
        setSurnameRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangeDateOfBirthVal = (val) => {
        setDateOfBirdthRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }

    const onClickRegister = () =>{
        
    } 
    return(
        <div className="content-data">
            <div>
                <h1>Dodaj administratora</h1>
                <div className='accountFormInputs'>
                        <GreenInput label="Login" error={errorStatus} value={loginRegVal} helperText={errorText} onChange={(e)=>onChangeLoginVal(e.target.value)}/>
                        <GreenInput label="Hasło" error={errorStatus} value={passwordRegVal} onChange={(e)=>onChangePasswordVal(e.target.value)} type='password'/>
                        <GreenInput label="Email" error={errorStatus} value={emailRegVal} helperText={errorText} onChange={(e)=>onChangeEmailVal(e.target.value)}/>
                        <GreenInput label="Imię"  error={errorStatus} value={nameRegVal} onChange={(e)=>onChangeNameVal(e.target.value)}/>
                        <GreenInput label="Nazwisko" error={errorStatus} value={surnameRegVal} onChange={(e)=>onChangeSurnameVal(e.target.value)}/>
                        <GreenInput type="date" label="Data urodzenia" error={errorStatus} value={dateOfBirthRegVal} onChange={(e)=>onChangeDateOfBirthVal(e.target.value)} fullWidth></GreenInput>

                    </div>
                    <div className='buttonsAccountMenu'>
                        <div className='accountFormButtons'>
                            <button className='main-btn accountFormButton' onClick={()=>onClickRegister()}>Zarejestruj</button>
                        </div>
                    </div>
            </div>
        </div>
    )
}
export default function AddAdmin(){
    const navigate = useNavigate()
    const [ready,setReady] = useState(false);

    useEffect(()=>{
        if(checkIsLogged()==='1')
            setReady(true);
        else
            navigate("/home");
    })
    return(
        <div className="App">
            <Header/>
            {
                ready?
                <main className='content'>
                    <AddAdminForm/>
                </main>
                :
                null
            }
                
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>
    )
}