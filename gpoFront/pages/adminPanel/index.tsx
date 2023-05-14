import type { NextPage } from "next";
import { useState } from "react";
import adminApi from "../../components/api/adminApi";
import { Accordeon } from "../../components/atoms/accordion";
import { Invisible } from "../../components/icons/invisible";
import { Visible } from "../../components/icons/visible";
import { Roles } from "../../components/interfaces/roles";
import { useModal } from "../../components/layout/ModalLayout";
import StatusAuth from "../../components/modals/statusAuth";

const adminPanel: NextPage = () => {
  const statusAuth = useModal();
  const [modalMessage, setModalMessage] = useState("");
  
  const [username, setUsername] = useState("");
  const handleInputUsername = (e: any) => {
    setUsername(e.target.value);
  };

  const [email, setEmail] = useState("");
  const handleInputEmail = (e: any) => {
    setEmail(e.target.value);
  };

  const [password, setPassword] = useState("");
  const [isMasked, setIsMasked] = useState(true);
  const handleInputPassword = (e: any) => {
    setPassword(e.target.value);
  };
  const toggleMask = () => {
    setIsMasked(!isMasked);
  };

  const [userRole, setUserRole] = useState(0);

  const [users, setUsers] = useState<{id: string, userName: string, email: string}[]>([])
  const [userInfo, setUserInfo] = useState<{
    user: string,
    groupName?: string,
    departmentName?: string,
    courseName?: string,
    studyProgramName?: string,
    facultyName?: string,
    studyTypeName?: string
  }>()

  const createUser = async () => {
    if(username && email && password) {
      adminApi.createUser(username, email, password, userRole)
      .then((res) => {
        setModalMessage(res.message)
        statusAuth.open()
      })
      .catch((error) => {
        if(typeof(error.response.data.errorMessage) === "object") {
          let temp: string[] = []
          error.response.data.errorMessage.map((item: {code: string, description:string}) => {
            temp.push(item.description)
          })
          setModalMessage(temp.join('\n'))
        } else setModalMessage(error.response.data.errorMessage)
        statusAuth.open()
      })
    } else {
      setModalMessage('Заполните все поля!')
      statusAuth.open()
    }
  }

  const deleteUser = async () => {
    if(username) {
      adminApi.deleteUser(username)
      .then((res) => {
        setModalMessage(res.message)
        statusAuth.open()
      })
      .catch((error) => {
        if(typeof(error.response.data.errorMessage) === "object") {
          let temp: string[] = []
          error.response.data.errorMessage.map((item: {code: string, description:string}) => {
            temp.push(item.description)
          })
          setModalMessage(temp.join('\n'))
        } else setModalMessage(error.response.data.errorMessage)
        statusAuth.open()
      })
    } else {
      setModalMessage('Заполните все поля!')
      statusAuth.open()
    }
  }

  const changeUserPassword = async () => {
    if(username && password){
      adminApi.changeUserPassword(username, password)
      .then((res) => {
        setModalMessage(res.message)
        statusAuth.open()
      })
      .catch((error) => {
        if(typeof(error.response.data.errorMessage) === "object") {
          let temp: string[] = []
          error.response.data.errorMessage.map((item: {code: string, description:string}) => {
            temp.push(item.description)
          })
          setModalMessage(temp.join('\n'))
        } else setModalMessage(error.response.data.errorMessage)
        statusAuth.open()
      })
    } else {
      setModalMessage('Заполните все поля!')
      statusAuth.open()
    }
  }

  const getUsers = async () => {
    adminApi.getUsers()
    .then((res) => {
      setUsers(res)
    })
    .catch((error) => {
      if(typeof(error.response.data.errorMessage) === "object") {
        let temp: string[] = []
        error.response.data.errorMessage.map((item: {code: string, description:string}) => {
          temp.push(item.description)
        })
        setModalMessage(temp.join('\n'))
      } else setModalMessage(error.response.data.errorMessage)
      statusAuth.open()
    })
  }

  const getUsersByUsername = async () => {
    if(username) {
      adminApi.getUserByUsername(username)
      .then((res) => {
        console.log(res)
        setUserInfo(res)
      })
      .catch((error) => {
        if(typeof(error.response.data.errorMessage) === "object") {
          let temp: string[] = []
          error.response.data.errorMessage.map((item: {code: string, description:string}) => {
            temp.push(item.description)
          })
          setModalMessage(temp.join('\n'))
        } else setModalMessage(error.response.data.errorMessage)
        statusAuth.open()
      })
    }
  }

  return (
    <>
      <StatusAuth statusAuth={statusAuth} message={modalMessage} />
      <div className="bg-gray-main h-screen w-full grid grid-cols-12 gap-4">
        <div className="col-start-2 col-span-10 bg-white mt-24 rounded-lg p-6 flex flex-col h-[80vh]">
          <p className="text-center text-2xl pb-1 mb-6 w-max self-center">
            Панель администратора
          </p>
          <div className="h-[90%] overflow-y-scroll border rounded-lg p-4">

            {/* Получение пользователя по логину */}
            <Accordeon name="Получение информации о пользователе" className="">
              <div className="flex flex-col">
                {/* Имя пользователя */}
                <div className="flex items-center">
                  <p className="whitespace-nowrap text-lg">
                    Имя пользователя:{" "}
                  </p>
                  <input
                    value={username}
                    onChange={handleInputUsername}
                    placeholder="kremlev430-4"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
                </div>

                <button 
                onClick={() => getUsersByUsername()}
                disabled={!username}
                className="mt-4 w-max border-2 self-center p-2 rounded-md hover:border-blue-active disabled:border-gray-500 disabled:bg-gray-500">
                  Получить информацию о пользователе
                </button>

                {userInfo && (
                  <div className={`h-full p-1 border-2 rounded-md border-black mt-4`}>
                    <div className="flex flex-col items-center rounded-md p-2 first:mt-0 mt-2">
                      <p>Пользователь: {userInfo.user}</p>
                      {userInfo.groupName && (<p>Группа: {userInfo.groupName}</p>)}
                      {userInfo.departmentName && (<p>Кафедра: {userInfo.departmentName}</p>)}
                      {userInfo.courseName && (<p>Курс: {userInfo.courseName}</p>)}
                      {userInfo.studyProgramName && (<p>Степень: {userInfo.studyProgramName}</p>)}
                      {userInfo.facultyName && (<p>Факультет: {userInfo.facultyName}</p>)}
                      {userInfo.studyTypeName && (<p>Тип: {userInfo.studyTypeName}</p>)}
                    </div>
                </div>
                )}

              </div>
            </Accordeon>

            {/* Получение списка пользователей */}
            <Accordeon name="Получение списка пользователей" className="mt-4">
              <div className="flex flex-col">
                <div className={`${users.length !== 0 ? 'h-40' : 'h-8'} overflow-y-scroll p-1 border-2 rounded-md border-black`}>
                  {users.length !== 0 && users.map((item, index) => (
                    <div key={index} className="flex flex-col items-center border border-gray-700 rounded-md p-2 first:mt-0 mt-2">
                      <p>id: {item.id}</p>
                      <p>username: {item.userName}</p>
                      {item.email && <p>email: {item.email}</p>}
                    </div>
                  ))}
                </div>

                <button 
                onClick={() => getUsers()}
                className="mt-4 w-max border-2 self-center p-2 rounded-md hover:border-blue-active disabled:border-gray-500 disabled:bg-gray-500">
                  Получить список пользователей
                </button>
              </div>
            </Accordeon>

            {/* Создание пользователя */}
            <Accordeon name="Создание пользователя" className="mt-4">
              <div className="flex flex-col">
                {/* Имя пользователя */}
                <div className="flex items-center">
                  <p className="whitespace-nowrap text-lg">
                    Имя пользователя:{" "}
                  </p>
                  <input
                    value={username}
                    onChange={handleInputUsername}
                    placeholder="kremlev430-4"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
                </div>

                {/* email */}
                <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">Email: </p>
                  <input
                    value={email}
                    onChange={handleInputEmail}
                    placeholder="test@mail.ru"
                    type="email"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
                </div>

                {/* password */}
                <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">Пароль: </p>
                  <div className="ml-2 w-full border p-1 rounded-lg text-base flex items-center">
                    <input
                      type={isMasked ? "password" : "text"}
                      className="bg-transparent outline-none placeholder:text-menu-text-gray w-full"
                      value={password}
                      placeholder={"*******"}
                      onChange={handleInputPassword}
                    />
                    <div
                      className="cursor-pointer h-6 w-6 hover:text-blue-active"
                      onClick={toggleMask}
                    >
                      {isMasked ? <Invisible /> : <Visible />}
                    </div>
                  </div>
                </div>

                {/* role */}
                <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">Роль: </p>
                  <div className="flex ml-2">
                    {Roles.map((item, index) => (
                      <div
                        onClick={() => setUserRole(index)}
                        className={`
                    ${
                      index === userRole
                        ? "border-blue-active text-blue-active"
                        : "border-gray-200 text-black cursor-pointer"
                    }
                    mr-2 last:mr-0 p-1 text-base border-2 rounded-md`}
                      >
                        {item}
                      </div>
                    ))}
                  </div>
                </div>

                <button 
                onClick={() => createUser()}
                disabled={!username || !email || !password}
                className="mt-4 w-max border-2 self-center p-2 rounded-md hover:border-blue-active disabled:border-gray-500 disabled:bg-gray-500">
                  Создать пользователя
                </button>
              </div>
            </Accordeon>

            {/* Удаление пользователя */}
            <Accordeon name="Удаление пользователя" className="mt-4">
              <div className="flex flex-col">
                {/* Имя пользователя */}
                <div className="flex items-center">
                  <p className="whitespace-nowrap text-lg">
                    Имя пользователя:{" "}
                  </p>
                  <input
                    value={username}
                    onChange={handleInputUsername}
                    placeholder="kremlev430-4"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
                </div>

                <button 
                onClick={() => deleteUser()}
                disabled={!username}
                className="mt-4 w-max border-2 self-center p-2 rounded-md hover:border-blue-active disabled:border-gray-500 disabled:bg-gray-500">
                  Удалить пользователя
                </button>
              </div>
            </Accordeon>

            {/* Смена пароля пользователя */}
            <Accordeon name="Смена пароля пользователя" className="mt-4">
              <div className="flex flex-col">
                {/* Имя пользователя */}
                <div className="flex items-center">
                  <p className="whitespace-nowrap text-lg">
                    Имя пользователя:{" "}
                  </p>
                  <input
                    value={username}
                    onChange={handleInputUsername}
                    placeholder="kremlev430-4"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
                </div>

                {/* new password */}
                <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">Новый пароль: </p>
                  <div className="ml-2 w-full border p-1 rounded-lg text-base flex items-center">
                    <input
                      type={isMasked ? "password" : "text"}
                      className="bg-transparent outline-none placeholder:text-menu-text-gray w-full"
                      value={password}
                      placeholder={"*******"}
                      onChange={handleInputPassword}
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
                onClick={() => changeUserPassword()}
                disabled={!username || !password}
                className="mt-4 w-max border-2 self-center p-2 rounded-md hover:border-blue-active disabled:border-gray-500 disabled:bg-gray-500">
                  Сменить пароль пользователя
                </button>
              </div>
            </Accordeon>

          </div>
        </div>
      </div>
    </>
  );
};

export default adminPanel;
