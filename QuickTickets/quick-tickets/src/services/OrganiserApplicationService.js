import axios from "axios";
import { HostName } from "./HostName";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
export default class OrganiserApplicationService{
    async addOrganiserApplication(userID,description){
        try{
            const response = await axios.post(HostName+'/api/OrganiserApplication/SendOrganiserApplication',
            {
                userId: userID,
                description: description
            }
            )
            return response
        }catch(e)
        {
            return e.response
        }
    }
    async getOrganiserApplication(pageIndex,pageSize){
        

        try{
            const response = await axios.post(HostName+'/api/OrganiserApplication/GetPendingOrganiserApplications',
            {
                pageIndex: pageIndex,
                pageSize: pageSize
            }
            )
            return response
        }catch(e)
        {
            return e.response
        }
    }
    async acceptOrganiserApplication(id){
        try{
            const response = await axios.post(HostName+'/api/OrganiserApplication/AcceptOrganiserApplication',id, {
                headers: {
                  'Content-Type': 'application/json',
                  Accept: '*/*',
                }
            })
            return response

        }catch(e)
        {
            return e.response
        }
    }
    async cancelOrganiserApplication(id){
        try{
            const response = await axios.post(HostName+'/api/OrganiserApplication/CancelOrganiserApplication',id, {
                headers: {
                  'Content-Type': 'application/json',
                  Accept: '*/*',
                }
            })
            return response

        }catch(e)
        {
            return e.response
        }
    }

}