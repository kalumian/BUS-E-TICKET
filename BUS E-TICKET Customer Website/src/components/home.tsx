import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const Home: React.FC = () => {
    const navigate = useNavigate();

    useEffect(() => {
        navigate('/trips');
    }, [navigate]);

    return null;
};

export default Home;