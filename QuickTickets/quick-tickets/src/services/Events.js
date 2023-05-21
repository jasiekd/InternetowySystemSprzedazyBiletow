import axios from "axios";
import {HostName} from "./HostName.js";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
export default class EventsService{
    async addEvent(data){
        console.log(data);

        try{
            const response = await axios.post(HostName+'/api/Events/addEvent',{
                title: data.title,
                seats: data.seats,
                ticketPrice: data.ticketPrice,
                description: data.description,
                date:data.date,
                isActive:true,
                adultsOnly:true,
                typeID:1,
                locationID:1,
                imgURL:data.imgURL
            });
           // console.log("test");
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
    async getTypesOfEvents(){
        try{
            const response = await axios.get(HostName+'/api/TypesOfEvents',{});
            return response
        }catch(error)
        {
            return error.response;
        }
    }
    async getEventLocations(){
        try{
            const response = await axios.get(HostName+'/api/Locations',{});
            return response;
        }catch(error)
        {
            return error.response;
        }
    }
    async addLocation(name){
        try{
            const response = await axios.post(HostName+'/api/Locations/addLocation',{
                locationID:0,
                name:name
            });
            return response;
        }catch(error)
        {
            return error.response;
        }
    }
    async addTypeOfevent(name){
        try{
            const response = await axios.post(HostName+'/api/TypeOfEvents/AddTypeOfEvent',{
                typeID:0,
                description:name
            });
            return response;
        }catch(error)
        {
            return error.response;
        }
    }
}