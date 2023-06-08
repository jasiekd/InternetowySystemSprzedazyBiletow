import axios from "axios";
import {HostName} from "./HostName.js";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
export default class EventsService{
    async addEvent(data){
        try{
            const response = await axios.post(HostName+'/api/Events/addEvent',{
                title: data.title,
                seats: data.seats,
                ticketPrice: data.ticketPrice,
                description: data.description,
                date:data.date,
                isActive:true,
                adultsOnly:data.adultsOnly,
                typeID:data.typeID,
                locationID:data.locationID,
                imgURL:data.imgURL
            });
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
    async search(searchPhrase,minPrice,maxPrice,startDate,endDate,locationId,typeId,pageIndex,pageSize){
        try{
            const response = await axios.post(HostName+'/api/Events/search',{
                searchPhrase: searchPhrase,
                minPrice: minPrice,
                maxPrice: maxPrice,
                startDate: startDate,
                endDate: endDate,
                locationId: locationId,
                typeId: typeId,
                pageIndex: pageIndex,
                pageSize: pageSize
            });
            return response;
        }catch(error)
        {
            return error.response;
        }
    }
    async getEvent(eventId){
        try{
            const response = await axios.get(HostName+'/api/Events/getEvent/'+eventId)
            return response;
        }catch(error){
            return error.response;
        }
    }
    async getPendingEvents(pageIndex,pageSize){
        try{
            const response = await axios.post(HostName+'/api/Events/GetPendingEvents',{
                pageIndex: pageIndex,
                pageSize: pageSize
              });
            return response;
        }catch(error)
        {
            return error.response;
        }
    }
    async acceptEvent(id){
        try{
            const response = await axios.post(HostName+"/api/Events/AcceptEvent",id,{
                headers: {
                    'Content-Type': 'application/json',
                    Accept: '*/*',
                  
                }
            })
            return response;
        }
        catch(error){
            return error.response
        }
    }
    async cancelEvent(id){
        try{
            const response = await axios.post(HostName+"/api/Events/CancelEvent",id,{
                headers: {
                    'Content-Type': 'application/json',
                    Accept: '*/*',
                  
                }
            })
            return response
        }
        catch(error){
            return error.response
        }
    }

    async getOrganisatorEvents(pageIndex,pageSize,status){
        try{
            const response = await axios.post(HostName+"/api/Events/GetOrganisatorEvents?statusChoice=Cancelled",{
                pageIndex: pageIndex,
                pageSize: pageSize
            })
            return response;
        }catch(error){
            return error.response;
        }
    }
}