import React, { useState, useEffect, useContext} from "react";


interface Props {
    children: any
}


// const AccountContext = React.createContext({
//     dealsList: list,
//     setDealsList: (arg:IDealsCard[]) => {},
// });

// const AccountContextProvider = ({ children }: Props) => {
//     const [dealsList, setDealsList] = useState<IDealsCard[]>([])
    
//     return (
//         <AccountContext.Provider
//             value={{
//                 dealsList,
//                 setDealsList,
//             }}
//         >
//             {children}
//         </AccountContext.Provider>
//     );
// };

// export default AccountContextProvider;

// export { AccountContext };