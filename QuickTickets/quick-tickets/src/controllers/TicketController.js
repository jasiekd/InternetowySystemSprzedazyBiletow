
export default function TicketController({children}){

    const gateway = new TicketController();

    const getMyTickets = async(choice, pageIndex,pageSize) =>{
        const response = await gateway.getMyTickets(choice,pageIndex,pageSize);

        if(response.status === 200)
        {
            return response.data;
        }
        else
        {
         
        }
    }
    const getMyTicket = async(ticketID)=>{
        const response = await gateway.getMyTicket(ticketID);

        if(response.status === 200)
        {
            return response.data;
        }
        else
        {
         
        }
    }

    return React.cloneElement(children,{
        getMyTickets,
        getMyTicket
    })
}