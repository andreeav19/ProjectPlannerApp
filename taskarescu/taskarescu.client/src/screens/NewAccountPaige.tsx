import React from 'react';
import Input from "../components/Input.tsx";
import Button from "../components/Button.tsx";
import Spacer from "../components/Spacer.tsx";
import "./NewAccountPaige.css"
import {useNavigate} from "react-router-dom";

const NewAccountPaige: React.FC = () => {
    const navigate = useNavigate();

    const redirectToHomePaige = () => {
        navigate('/home');
    };
    return (
        <div>
            <h2>Creaza-ti cont nou</h2>
            <Input label={"Nume"} placeholder={"nume"}/>
            <Input label={"Prenume"} placeholder={"prenume"}/>
            <Input label={"Email"} placeholder={"email"}/>
            <Input label={"Parola"} placeholder={"parola"}/>
            <Input label={"Confirma Parola"} placeholder={"rescrie parola"}/>

            <Spacer size={10}></Spacer>

            <Button onPress={redirectToHomePaige}>Submit</Button>
        </div>
    );
};

export default NewAccountPaige;
