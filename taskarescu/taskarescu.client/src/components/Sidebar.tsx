import { FaProjectDiagram, FaUser, FaTrophy, FaSignOutAlt, FaUserCircle, FaEnvelope } from "react-icons/fa";
import "./Sidebar.css";
import {useNavigate} from "react-router-dom";
import Spacer from "./Spacer.tsx";

const Sidebar = () => {
    const navigate = useNavigate();

    const redirectToHomePaige = () => {
        navigate('/home');
    };
    const redirectProfileScreen = () => {
        navigate('/profile');
    };
    const redirectleaderboard = () => {
        navigate('/leaderboard');
    };
    const logout = () => {
        navigate('/');
    };

    return (
        <aside className="sidebar">
                <div className="logo">
                    <img style={{
                        height: 50,
                        width: 50,
                    }}
                         src={"/logo.png"}
                         alt="Logo"/>
                    <Spacer size={40}></Spacer>
                    <h2>Tăskărescu</h2>
                </div>
            <div className="tabs">
                <button className="tab" onClick={redirectToHomePaige}>
                    <FaProjectDiagram className="tab-icon"/>
                    <span className="tab-text">Proiectele mele</span>
                </button>
                <button className="tab" onClick={redirectleaderboard}>
                    <FaTrophy className="tab-icon"/>
                    <span className="tab-text">Leaderboard</span>
                </button>
                <button className="tab" onClick={redirectProfileScreen}>
                    <FaUser className="tab-icon"/>
                    <span className="tab-text">Profil</span>
                </button>
                <button className="tab text-red-500" onClick={logout}>
                    <FaSignOutAlt className="tab-icon"/>
                    <span className="tab-text">Log Out</span>
                </button>
            </div>
            <div className="user-info">
                <div className="user-icon">
                    <span className="text-xs text-gray-600">
                        <FaUserCircle className="user-icon"/>
                    </span>
                    <h4 className="font-semibold">Matache Alexandru</h4>
                </div>
                <div className="user-details">
                    <span className="text-xs text-gray-600">
                        <FaEnvelope className="info-icon" /> alexandrumatache300@yahoo.com
                    </span>
                    <span className="text-xs text-gray-600">Student</span>
                </div>
            </div>
        </aside>
    );
};

export default Sidebar;
