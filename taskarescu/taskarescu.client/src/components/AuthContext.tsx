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

export { AuthProvider, AuthContext };
