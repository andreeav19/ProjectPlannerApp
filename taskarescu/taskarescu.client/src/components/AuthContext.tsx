// AuthContext.tsx
import React, { createContext, useContext, useState, ReactNode } from "react";
import axios from "axios";
import Cookies from "js-cookie";
interface AuthContextProps {
  children: ReactNode;
}

type IAuthContext = {
  authenticated: boolean;
  setAuthicated: (newState: boolean) => void;
};

const initialValues = {
  authenticated: false,
  setAuthicated: () => {},
};

const AuthContext = createContext<IAuthContext>(initialValues);

const AuthProvider = ({ children }: AuthContextProps) => {
  const [authenticated, setAuthicated] = useState(false);

  return (
    <AuthContext.Provider value={{ authenticated, setAuthicated }}>
      {children}
    </AuthContext.Provider>
  );
};

export function getDecodedJWT() {
  const jwt = Cookies.get("jwtToken");
  const data = JSON.parse(atob(jwt?.split(".")[1] ?? ""));
  const decodedToken = {
    jwt: jwt,
    audience: data.aud,
    expiration: data.exp,
    emailAddress:
      data[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
      ],
    name: data["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
    nameIdentifier:
      data[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
      ],
    issuer: data.iss,
    jwtId: data.jti,
  };
  return decodedToken;
}

export { AuthProvider, AuthContext };
