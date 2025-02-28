import { CAlert } from "@coreui/react-pro";

const P_Success = ({text} : {text : string}) => {
    return(text && <CAlert color="primary">{text}</CAlert> )
}

export default P_Success;