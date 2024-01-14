import './App.css';
import Spacer from "./components/Spacer.tsx";
import Input from "./components/Input.tsx";
import Button from "./components/Button.tsx";
import {useNavigate} from "react-router-dom";

// import Login from './components/Login';

function App() {
    const navigate = useNavigate();

    const redirectToNewAccount = () => {
        navigate('/newaccount');
    };

    const redirectToHomeScreen = () => {
        navigate('/home');
    };


    return (
        <div>
            <img src="/logo.png" width="69%"/>
            <h1>Tăskărescu</h1>
            {/*<Login></Login>*/}

            <Input label={"Email"} placeholder={"email"}/>
            <Input label={"Parola"} placeholder={"parola"}/>

            <Spacer size={10}/>

            <div>
                <Button onPress={redirectToHomeScreen}>Login</Button>
                <Spacer size={5}/>
                <Button onPress={redirectToNewAccount}>Cont nou</Button>
            </div>
        </div>
    );
}

export default App;