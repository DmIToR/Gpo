import type { NextPage } from "next";
import { useRouter } from "next/router";
import { useContext, useState } from "react";
import { Invisible } from "../../components/icons/invisible";
import { Visible } from "../../components/icons/visible";
import { useModal } from "../../components/layout/ModalLayout";
import StatusAuth from "../../components/modals/statusAuth";
import { accountApi, profileApi } from "../../components/api";
import { ProfileUserContext, Roles } from "../../components/context/profileUserContext";

const Auth: NextPage = () => {
  const statusAuth = useModal();
  const router = useRouter();
  const {setRole, setProfileUserInfo} = useContext(ProfileUserContext)
  const [modalMessage, setModalMessage] = useState("");

  const [login, setLogin] = useState("");
  const handleLogin = (e: any) => {
    setLogin(e.target.value);
  };

  const [password, setPassword] = useState("");
  const [isMasked, setIsMasked] = useState(true);
  const handlePassword = (e: any) => {
    setPassword(e.target.value);
  };

  const toggleMask = () => {
    setIsMasked(!isMasked);
  };

  const authUser = async () => {
    accountApi.signIn(login, password)
    .then((result) => {
        if(login === 'admin') {
            router.push('/adminPanel')
            localStorage.setItem("auth", JSON.stringify({'id': 'admin', 'token': result.authToken}))
        }
        else {
            router.push('/profile')
            localStorage.setItem("auth", JSON.stringify({'id': result.id, 'token': result.authToken}))
            profileApi.getUserProfile(result.id, result.authToken)
            .then((res) => {
              setProfileUserInfo(res.profile);
              setRole(Roles[res.role])
            })
            .catch((error) => {
              console.error(error)
            })
        }
    }) 
    .catch((error) => {
        console.log(error)
        setModalMessage(error.response.data.errorMessage)
        statusAuth.open()
    })
  };

  return (
    <>
      <StatusAuth statusAuth={statusAuth} message={modalMessage} />
      <div className="bg-gray-main h-screen w-full grid grid-cols-12 gap-4">
        <div className="col-start-4 col-span-6 bg-white my-[20%] rounded-lg p-6 flex flex-col h-max">
          <div className="flex justify-center items-center mb-6">
            <div
              className={`cursor-pointer hover:text-blue-active border-b-2 border-blue-active text-blue-active
                py-2 mx-6 transition-all duration-500`}
            >
              Авторизация
            </div>
          </div>
          <div className="flex flex-col">
            <div className="grid grid-cols-[13%_87%] items-center pb-2">
              <p className="text-xl text-right">Логин: </p>
              <input
                type="text"
                className="ml-4 w-[80%] border-2 p-2 rounded-lg text-lg bg-transparent outline-none placeholder:text-menu-text-gray"
                value={login}
                placeholder={"nsTusur"}
                onChange={handleLogin}
              />
            </div>
            <div className="grid grid-cols-[13%_87%] items-center pb-2">
              <p className="text-xl text-right">Пароль: </p>
              <div className="ml-4 w-[80%] border-2 p-2 rounded-lg text-lg flex items-center">
                <input
                  type={isMasked ? "password" : "text"}
                  className="bg-transparent outline-none placeholder:text-menu-text-gray w-full"
                  value={password}
                  placeholder={"*******"}
                  onChange={handlePassword}
                />
                <div
                  className="cursor-pointer h-6 w-6 hover:text-blue-active"
                  onClick={toggleMask}
                >
                  {isMasked ? <Invisible /> : <Visible />}
                </div>
              </div>
            </div>
            <button
              onClick={() => authUser()}
              className="mt-4 border-2 w-max self-center px-6 py-2 rounded-lg hover:border-blue-active hover:text-blue-active 
            transition-all duration-500"
            >
              Войти
            </button>
          </div>
        </div>
      </div>
    </>
  );
};

export default Auth;
