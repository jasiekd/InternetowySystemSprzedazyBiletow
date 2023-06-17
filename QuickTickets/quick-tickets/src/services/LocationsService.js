import axios from "axios";
import { HostName } from "./HostName";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
export default class LocationsService{
    
    async addLocation(locationID,name,description,imgUrl){
        try{
            const response = await axios.post(HostName+'/api/Locations/addLocation',
            {
                locationID: locationID,
                name: name,
                description: description,
                imgURL: imgUrl
            })
            return response
        }catch(e)
        {
            return e.response
        }
    }
    async getLocation(id){
        try{
            const response = await axios.get(HostName+'/api/Locations/'+id,{})
            return response
        }catch(e)
        {
            return e.response
        }
    }
}