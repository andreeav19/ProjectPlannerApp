// AuthContext.tsx
import React, { createContext, useContext, useState, ReactNode } from "react";

interface AuthContextProps {
  children: ReactNode;
}

interface AuthContextValue {
  isAuthenticated: boolean;
  login: () => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

export const AuthProvider: React.FC<AuthContextProps> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  const login = () => {
    // Perform your authentication logic and set isAuthenticated to true
    setIsAuthenticated(true);
  };

  const logout = () => {
    // Perform your logout logic and set isAuthenticated to false
    setIsAuthenticated(false);
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
