import React, {createContext, useState} from 'react';
import {IJWTResponse} from "../dto/IJWTResponse";

const AuthContext = createContext<{
    jwtResponse: IJWTResponse | null,
    setJwtResponse: ((data: IJWTResponse | null) => void) | null,
}>({ jwtResponse: null, setJwtResponse: null});

export const AuthProvider = ({children}: {children: React.ReactNode}) => {

    const [jwtResponse, setJwtResponse] = useState(JSON.parse(localStorage.getItem("JWTResponse") || "null"));

    return (
        <AuthContext.Provider value={{jwtResponse, setJwtResponse}}>
            {children}
        </AuthContext.Provider>
    );
};
export default AuthContext;
