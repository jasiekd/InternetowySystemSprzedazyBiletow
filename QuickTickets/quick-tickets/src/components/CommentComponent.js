import Swal from "sweetalert2"
import { checkIsLogged } from "../controllers/Login"
import deleteIcon from "../images/delete.png"
import { useEffect, useState } from "react"
import Pagination from '@mui/material/Pagination';

export default function CommentComponent({ getComments,addComment,deleteComment,eventID}){
    const [pageCount,setPageCount] = useState(1);
    const [comments,setComments] = useState();
    const [updateComments,setUpdateComments] = useState(true);
    const showAddComent=()=>{
        const { value: text } = Swal.fire({
            input: 'textarea',
            inputLabel: 'Dodaj Komentarz',
            inputPlaceholder: 'Napisz co sądzisz o tym wydarzeniu...',
            inputAttributes: {
              'aria-label': 'Type your message here'
            },
            confirmButtonText: 'Zapisz',
            confirmButtonColor: '#93BB60',
            cancelButtonText: 'Anuluj',
            showCancelButton: true,
            showCloseButton: true
          }).then(result=>{
            if(result.isConfirmed){
                addComment(result.value,eventID).then(r=>{
                    setUpdateComments(!updateComments)
                });
                
            }
          })
          
        
    }
    const onDeleteComment = (id)=>{
        deleteComment(id).then(r=>{
            setUpdateComments(!updateComments);
        })
    }


    useEffect(()=>{
        getComments(eventID,pageCount,10).then((r)=>{
            setComments(r)
            console.log(r)
        })
       
    },[pageCount,updateComments])
    return(
        <div className='comments'>
                        <div className='event-title-header' style={{paddingLeft:"2rem",paddingTop:"1rem"}}>Komentarze<button className='main-btn' style={{marginRight:"0",marginLeft:"auto"}} onClick={showAddComent}>Dodaj komentarz</button></div>
                            <div className='comment-list'>
                                {
                                    comments?
                                        comments.comments.map((val,key)=>{
                                            return(
                                                <div className='comment' key={key}> 
                                                    <div className='comment-author'>{val.user.name+" "+val.user.surname}
                                                        {
                                                            checkIsLogged() === "1"?
                                                            <img src={deleteIcon} className='deleteComment' onClick={()=>onDeleteComment(val.commentID)}/>
                                                            :
                                                            null
                                                        }
                                                    </div>
                                                    <div className='comment-tetx'>{val.content} </div>
                                                    
                                                </div>
                                            )
                                        })
                                       
                                    :
                                    null
                                    
                                }
                                {
                                   comments?
                                        <Pagination count={Math.ceil(comments.totalCount/10)} size='large' onChange={(e,v)=>{setPageCount(v)}}/>
                                   :
                                   null
                                }
                                
                            </div>
                        </div>
    )
}