import React from "react";
import CommentService from "../services/CommentsService";
import Swal from "sweetalert2";

export default function CommentController({children}){
    const gateway = new CommentService();
    
    const getComments = async(eventID, pageIndex, pageSize) => {
        const response = await gateway.getComments(eventID,pageIndex,pageSize);

        if(response.status === 200)
        {
            return response.data;
        }
        else
        {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Nie można pobraż komentarzy',
                showConfirmButton: false,
                timer: 1500
              })
        }
    }
    const addComment = async(content,eventID) => {
        const response = await gateway.addComment(content,eventID);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Komentarz dodany',
                showConfirmButton: false,
                timer: 1500
              })
        }
        else
        {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Nie udało się dodać komentarza',
                showConfirmButton: true
              })
        }
    }
    const deleteComment = async(eventID) => {
        const response = await gateway.deleteComment(eventID);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Komentarz usunięty',
                showConfirmButton: false,
                timer: 1500
              })
            return true;
        }
        else
        {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Nie udało się usunąć komentarza',
                showConfirmButton: true
              })
            return false;
        }
    }

    return React.cloneElement(children,{
        getComments,
        addComment,
        deleteComment
    })
}