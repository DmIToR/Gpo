import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";
import { ProfileUserContext } from "../context/profileUserContext";
import { Profile } from "../icons/profile";

export const links: Array<{ name: string; link: string }> = [
  { name: "Главная", link: "/" },
  { name: "Документы", link: "/docs" },
  { name: "Компании", link: "/company" },
  { name: "Профиль", link: "/profile" },
];

export default function Header() {
  const [isAuth, setIsAuth] = useState(true);
  const [isOpenDropDown, setIsOpenDropDown] = useState(false);
  const [showLangs, setShowLangs] = useState(false);
  const {profileUserInfo} = useContext(ProfileUserContext)
  const router = useRouter();

  const toggleLangBar = () => {
    if (!isOpenDropDown) {
      setIsOpenDropDown(true);
      setTimeout(() => setShowLangs(true), 500);
    } else if (isOpenDropDown) {
      setShowLangs(false);
      setTimeout(() => setIsOpenDropDown(false), 500);
    }
  };

  const unAuth = () => {
    localStorage.removeItem("auth")
    router.push("/auth");
    setShowLangs(false);
    setIsOpenDropDown(false);
  }

  useEffect(() => {
    if (!localStorage.getItem("auth")) {
      setIsAuth(true);
    } else setIsAuth(false);
  }, [isAuth, router]);

  return (
    <div className="fixed top-0 bg-white w-full px-16 flex justify-between">
      <div className="flex items-center">
        {!isAuth ? (
          links.map((item, index) => (
            <div
              key={index}
              onClick={() => router.push(`${item.link}`)}
              className={`
            ${item.link && "cursor-pointer hover:text-blue-active"} 
            ${
              router.asPath === item.link
                ? "border-b-2 border-blue-active text-blue-active"
                : "text-black"
            }
            py-6 mx-6 transition-all duration-500`}
            >
              {item.name}
            </div>
          ))
        ) : (
          <div
            onClick={() => router.push(`/auth`)}
            className={`
            ${"/auth" && "cursor-pointer hover:text-blue-active"} 
            ${
              router.asPath === "/auth"
                ? "border-b-2 border-blue-active text-blue-active"
                : "text-black"
            }
            py-6 mx-6 transition-all duration-500`}
          >
            Аутентификация
          </div>
        )}
      </div>
      {!isAuth && (
        <div className={`flex items-center relative mr-6`}>
          <div onClick={toggleLangBar} className="flex group">
            <p className="group-hover:text-blue-active cursor-pointer">
              {`${profileUserInfo.surname} ${profileUserInfo.name.slice(0,1) + '.' + profileUserInfo.patronymic.slice(0,1)}`}
            </p>
            <div
              className={`text-black group-hover:text-blue-active active:text-blue-active 
                cursor-pointer h-6 w-6 ml-2`}
            >
              <Profile />
            </div>
          </div>

          <div
            className={`${
              isOpenDropDown ? "h-10" : "h-0"
            } transition-all duration-500
            absolute w-full bg-white top-[74px] z-10 rounded-b-lg`}
          >
            <div
              className={`${
                showLangs
                  ? "showContentNavBar opacity-100"
                  : "hideContentNavBar opacity-0"
              } transition-all duration-500`}
              onClick={() => unAuth()}
            >
              <p
                className={`text-center p-2 cursor-pointer hover:text-blue-active`}
              >
                Выйти
              </p>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
