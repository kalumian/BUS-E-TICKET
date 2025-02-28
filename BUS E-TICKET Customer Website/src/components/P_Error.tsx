import { CAlert } from "@coreui/react-pro";

const P_Error = ({text} : {text : string}) => {
    return(text && <CAlert color="danger">{text}</CAlert> )
}

export default P_Error;