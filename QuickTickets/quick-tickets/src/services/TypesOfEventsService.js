import axios from "axios";
import { HostName } from "./HostName";

export default class TypesOfEventsService{
    async addCategory(typeID,description){
        try{
            const response = await axios.post(HostName+'/api/TypesOfEvents/AddTypeOfEvent',
            {
                typeID: typeID,
                description: description
            })
            return response;
        }catch(e)
        {
            return e.response;
        }

    }
}