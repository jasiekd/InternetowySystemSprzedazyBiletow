import axios from "axios";
import {HostName} from "./HostName.js";

export default class EventsService{
    async addEvent(data){
        console.log(data);

        try{
            const response = await axios.get(HostName+'/api/Events/addEvent',{
                title: data.title,
                seats: data.seats,
                ticketPrice: data.ticketPrice,
                description: data.description,
                date: data.date,
                isActive: true,
                adultsOnly: data.adultsOnly,
                typeID: 0,
                locationID: 0,
                imgURL: data.imgURL
            });
            console.log(response);
            return response; 
        }catch(error)
        {
            return error.response;
        }
    }

    async getHotEvents(){
        try{
            const response = await axios.get(HostName+'/api/Events/getHotEvents',{});
            return response; 
        }catch(error)
        {
            return error.response;
        }
    }
    async getHotLocations(){
        try{
            const response = await axios.get(HostName+'/api/Events/getHotLocations',{});
            return response; 
        }catch(error)
        {
            return error.response;
        }
    }
}