import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom"

export default function Redirect(){
    const location = useLocation();
    const navigate = useNavigate();
    useEffect(()=>{
        const pathname = location.pathname;
        const id = pathname.split('/')[2];
        navigate("/buy-ticket",{state:{id:id}});
    },[location])
    return(
        <div>test</div>
    )
}