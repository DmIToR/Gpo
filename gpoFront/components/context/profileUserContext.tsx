import React, { useState, useEffect } from "react";
import { profileApi } from "../api";
import { ProfileUserDto } from "../interfaces/profileDto";

interface Props {
  children: any;
}

const ProfileUserContext = React.createContext<{
  profileUserInfo: ProfileUserDto;
  setProfileUserInfo: any;
}>({
  profileUserInfo: {
    group: "",
    id: "",
    name: "",
    surname: "",
    patronymic: "",
  },
  setProfileUserInfo: (arg: ProfileUserDto) => {},
});

const ProfileUserContextProvider = ({ children }: Props) => {
  const [idLocal, setIdLocal] = useState("");
  const [profileUserInfo, setProfileUserInfo] = useState<ProfileUserDto>({
    group: "",
    id: "",
    name: "",
    surname: "",
    patronymic: "",
  });

  useEffect(() => {
    //@ts-ignore
    let tempRes: { id: string; token: string } = localStorage.getItem("auth"); //@ts-ignore
    if (!tempRes) setIdLocal(tempRes?.id);
    else setIdLocal("null");
  }, []);

  useEffect(() => {
    //@ts-ignore
    let res = JSON.parse(localStorage.getItem("auth"));
    if (!res) {
      if (
        res?.id !== "null" &&
        res?.id !== "nologin" &&
        res?.id !== "admin" &&
        res?.id
      ) {
        profileApi
          .getUserProfile(res?.id, res?.token)
          .then((res) => {
            console.log(res);
            setProfileUserInfo(res);
          })
          .catch((error) => {
            console.error(error.response.data.errorMessage);
          });
      }
    }
  }, [idLocal, profileUserInfo.id]);

  return (
    <ProfileUserContext.Provider
      value={{
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
