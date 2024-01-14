import React from "react";
import "./Input.css";
// import PropTypes from "prop-types";

interface InputProps {
    label: string;
    placeholder: string;
    inputType?: string;
}

const Input: React.FC<InputProps> = ({ label, placeholder, inputType = "text" }) => {
    return (
        <div className="holeInput">
            <label className="textInput">{label}</label>
            <div className="styledInputs">
                <input className="genericInput" type={inputType} placeholder={placeholder} />
            </div>
        </div>
    );
};

// Input.propTypes = {
//     label: PropTypes.string.isRequired,
//     placeholder: PropTypes.string.isRequired,
//     inputType: PropTypes.string,
// };

export default Input;
