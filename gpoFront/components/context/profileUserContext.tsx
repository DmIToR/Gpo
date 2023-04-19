import React, { useState, useEffect } from "react";
import { profileApi } from "../api";
import { ProfileUserInfo } from "../interfaces/profileDto";

export const Roles = [
  'Студент',
  'Руководитель',
  'Управление образованием',
  'Секретарь'
]

interface Props {
  children: any;
}

const ProfileUserContext = React.createContext<{
  profileUserInfo: ProfileUserInfo;
  setProfileUserInfo: any;
  role: string,
  setRole: any
}>({
  profileUserInfo: {
    id: "",
    name: "",
    surname: "",
    patronymic: "",
  },
  setProfileUserInfo: (arg: ProfileUserInfo) => {},
  role: '',
  setRole: (arg: string) => {}
});

const ProfileUserContextProvider = ({ children }: Props) => {
  const [role, setRole] = useState('')
  const [profileUserInfo, setProfileUserInfo] = useState<ProfileUserInfo>({
    id: "",
    name: "",
    surname: "",
    patronymic: "",
  });

  useEffect(() => {
    //@ts-ignore
    let res = JSON.parse(localStorage.getItem("auth"));
    if (res) {
      if (
        res.id &&
        res.id !== "admin"
      ) {
        profileApi
          .getUserProfile(res.id, res.token)
          .then((res) => {
            setProfileUserInfo(res.profile);
            setRole(Roles[res.role])
          })
          .catch((error) => {
            console.error(error.response.data.errorMessage);
          });
      }
    }
  }, []);

  return (
    <ProfileUserContext.Provider
      value={{
        role,
        setRole,
        profileUserInfo,
        setProfileUserInfo,
      }}
    >
      {children}
    </ProfileUserContext.Provider>
  );
};

export default ProfileUserContextProvider;

export { ProfileUserContext };
