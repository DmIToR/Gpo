import axios from "axios";
import type { NextPage } from "next";
import { useState } from "react";
import { Invisible } from "../../components/icons/invisible";
import { Visible } from "../../components/icons/visible";
import { useModal } from "../../components/layout/ModalLayout";
import StatusAuth from "../../components/modals/statusAuth";

const authTabs: Array<{ name: string; state: boolean }> = [
  { name: "Авторизация", state: true },
  { name: "Регистрация", state: false },
];

const Auth: NextPage = () => {
  const [isAuth, setIsAuth] = useState(true);
  const statusAuth = useModal()
  const [modalMessage, setModalMessage] = useState('')

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

  const addUser = async () => {
    try {
        await axios.post(process.env.NEXT_PUBLIC_ENVNAME_API + `User/AddUser?username=${login}&password=${password}`)
        .then((response) => {
            // console.log(response)
            if(!response.data.isContains) {
                setModalMessage(response.data.message)
                statusAuth.open()
            } else {
                setModalMessage(response.data.message)
                const temp = {
                    'login' : login,
                    'pass' : password
                }
                localStorage.setItem('auth', JSON.stringify(temp))
                // var tempObject = JSON.parse(retrievedObject);
                statusAuth.open()
            }
        })
        .catch((response) => {
            console.log(response)
        })
    } catch (e: any) {
        console.log(e.message);
    }
  }

  const authUser = async () => {
    try {
        await axios.get(process.env.NEXT_PUBLIC_ENVNAME_API + `User/AuthorizeUser?username=${login}&password=${password}`)
        .then((response) => {
            // console.log(response)
            if(!response.data.isContains) {
                setModalMessage(response.data.message)
                statusAuth.open()
            } else {
                setModalMessage(response.data.message)
                const temp = {
                    'login' : login,
                    'pass' : password
                }
                localStorage.setItem('auth', JSON.stringify(temp))
                statusAuth.open()
            }
        })
        .catch((response) => {
            console.log(response)
        })
    } catch (e: any) {
        console.log(e.message);
    }
  }

  return (
    <>
        <StatusAuth 
            statusAuth={statusAuth}
            message={modalMessage}
        />
        <div className="bg-gray-main h-screen w-full grid grid-cols-12 gap-4">
        <div className="col-start-4 col-span-6 bg-white my-[20%] rounded-lg p-6 flex flex-col h-max">
            <div className="flex justify-center items-center mb-6">
            {authTabs.map((item, index) => (
                <div
                key={index}
                onClick={() => setIsAuth(item.state)}
                className={`cursor-pointer hover:text-blue-active
                        ${
                        item.state === isAuth
                            ? "border-b-2 border-blue-active text-blue-active"
                            : "text-black"
                        }
                        py-2 mx-6 transition-all duration-500`}
                >
                {item.name}
                </div>
            ))}
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
                    <div className="cursor-pointer h-6 w-6 hover:text-blue-active" onClick={toggleMask}>
                        {isMasked ? <Invisible /> : <Visible />}
                    </div>
                </div>
            </div>
            <button 
            onClick={() => {
                isAuth ? authUser()
                : addUser()
            }}
            className="mt-4 border-2 w-max self-center px-6 py-2 rounded-lg hover:border-blue-active hover:text-blue-active 
            transition-all duration-500">
                {isAuth ? 'Войти' : 'Зарегистрироваться'}
            </button>
            </div>
        </div>
        </div>
    </>
    
  );
};

export default Auth;
