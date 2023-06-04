import axios from "axios";
import { HostName } from "./HostName";
import jwtDecode from "jwt-decode";
export default class AccountService{
    async login(data){
        try{
            const response = await axios.post(HostName+'/api/Account/login',
            {
                userName: data.loginVal,
                password: data.passwordVal
            })
            localStorage.accessToken = response.data.accessToken;
            localStorage.refreshToken = response.data.refreshToken;

            const rawDecodedToken = jwtDecode(response.data.accessToken);
            localStorage.roleId = rawDecodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            localStorage.uId = rawDecodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            console.log(localStorage.roleId);
            
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

            const rawDecodedToken = jwtDecode(response.data.accessToken);
            localStorage.roleId = rawDecodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            localStorage.uId = rawDecodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            console.log(localStorage.roleId);

            axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.accessToken}`;
            // this.getUser();
            return response;
        }catch(error){
            return error.response
        }
    }
    async register(data)
    {
        try{
            const response = await axios.post(HostName+'/api/Account/register',
            {
                name: data.nameRegVal,
                surname: data.surnameRegVal,
                email: data.emailRegVal,
                login: data.loginRegVal,
                password: data.passwordRegVal,
                dateOfBirth: data.dateOfBirthRegVal
            })

            return response;
        }
        catch(error){
            return error.response;
        }
    }
    async getUser()
    {
        
        try{
            const response = await axios.get(HostName+'/api/Account/getUser',{})
            console.log(response)
            return response;
        }
        catch(error){
            return error.response;
        }
    }
}