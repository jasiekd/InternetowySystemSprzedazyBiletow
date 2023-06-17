import axios from "axios";
import { HostName } from "./HostName";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
export default class CommentService{

    async getComments(eventID, pageIndex, pageSize){
        try{
            const response = axios.post(HostName+"/api/Comment/GetComments?eventID="+eventID,{
                pageIndex: pageIndex,
                pageSize: pageSize
            })
            return response;
        }catch(error)
        {
            return error.response;
        }
    }
    async addComment(content,eventID){
        try{
            const response = axios.post(HostName+"/api/Comment/AddComment",{
                content: content,
                eventID: eventID
            })
            return response;
        }catch(error)
        {
            return error.response;
        }
    }
    async deleteComment(commentID){
        try{
            const response = axios.delete(HostName+"/api/Comment/"+commentID,{})
            return response;
        }catch(error)
        {
            return error.response;
        }
    }

}