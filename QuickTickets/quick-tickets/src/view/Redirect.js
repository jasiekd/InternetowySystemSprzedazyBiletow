import { useEffect } from "react";
import { useLocation } from "react-router-dom"

export default function Redirect(){
    const location = useLocation();
    useEffect(()=>{
        console.log(location);
        const pathname = location.pathname;
        const id = pathname.split('/')[2];
        window.location.href = "http://localhost:3000/buy-ticket?id="+id;
    },[location])
    return(
        <div>
        </div>
    )
}