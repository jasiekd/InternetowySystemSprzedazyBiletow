import axios from "axios";
import {HostName} from "./HostName.js";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
export default class UserEventHistoryService{
    
    async getPredictedEvents()
    {
        try{
            const response = await axios.get(HostName+"/api/UserEventHistory/GetPredictedEvents")
            return response;
        }catch(error){
            return error.response;
        }
    }

}