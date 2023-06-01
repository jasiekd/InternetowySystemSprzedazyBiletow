import axios from "axios";
import { HostName } from "./HostName";

export default class LocationsService{
    async addLocation(locationID,name,description,imgUrl){
        try{
            const response = await axios.post(HostName+'/api/Locations/addLocation',
            {
                locationID: locationID,
                name: name
            })
            console.log(response)
            return response
        }catch(e)
        {
            return e.response
        }
    }
}