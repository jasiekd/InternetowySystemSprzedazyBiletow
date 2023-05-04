
import axios from "axios";
import {HostName} from "./HostName.js";
export default class LoginService{
    async login(data){
       try{
           const response = await axios.post(HostName+'/api/Account/login',
           {
               userName: data.loginVal,
               password: data.passwordVal
           })
           localStorage.accessToken = response.data.accessToken;
           localStorage.refreshToken = response.data.refreshToken;
           axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
           return response;
       }catch(error)
       {
           return error.response;
       }
       
   }
   async loginWithGoogle(data){
       try{
           const response = await axios.post(HostName+"/api/Account/loginWithGoogle",
           {
               credentials: data.credential,
               clientID: data.clientId
           })
           localStorage.accessToken = response.data.accessToken;
           localStorage.refreshToken = response.data.refreshToken;
           axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
           return response;
       }catch(error){
           return error.response
       }
   }
}