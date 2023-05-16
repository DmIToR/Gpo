import type { NextPage } from "next";
import { useContext, useState } from "react";
import { accountApi } from "../../components/api";
import { ProfileUserContext } from "../../components/context/profileUserContext";
import { useModal } from "../../components/layout/ModalLayout";
import StatusAuth from "../../components/modals/statusAuth";

const Documents: NextPage = () => {
  const statusAuth = useModal();
  const [modalMessage, setModalMessage] = useState("");
  const { profileUserInfo } = useContext(ProfileUserContext)
  const [typeDocument, setTypeDocument] = useState(0)

  const [fullnameCompany, setFullnameCompany] = useState("");
  const handleInputFullnameCompany= (e: any) => {
    setFullnameCompany(e.target.value);
  };

  const [contactJobName, setcontactJobName] = useState("");
  const handleInputcontactJobName= (e: any) => {
    setcontactJobName(e.target.value);
  };

  const [contactName, setcontactName] = useState("");
  const handleInputcontactName= (e: any) => {
    setcontactName(e.target.value);
  };

  const [contactLastname, setcontactLastname] = useState("");
  const handleInputcontactLastname= (e: any) => {
    setcontactLastname(e.target.value);
  };

  const [contactPatronymic, setcontactPatronymic] = useState("");
  const handleInputcontactPatronymic= (e: any) => {
    setcontactPatronymic(e.target.value);
  };

  const [studyProgramName, setstudyProgramName] = useState("");
  const handleInputstudyProgramName= (e: any) => {
    setstudyProgramName(e.target.value);
  };

  const [companyAddress, setcompanyAddress] = useState("");
  const handleInputcompanyAddress= (e: any) => {
    setcompanyAddress(e.target.value);
  };

  const [studyCourseName, setstudyCourseName] = useState("");
  const handleInputstudyCourseName= (e: any) => {
    setstudyCourseName(e.target.value);
  };

  const [practiceTypeName, setpracticeTypeName] = useState("");
  const handleInputpracticeTypeName= (e: any) => {
    setpracticeTypeName(e.target.value);
  };

  const [practiceKindName, setpracticeKindName] = useState("");
  const handleInputpracticeKindName= (e: any) => {
    setpracticeKindName(e.target.value);
  };

  const [practiceStart, setpracticeStart] = useState("");
  const handleInputpracticeStart= (e: any) => {
    setpracticeStart(e.target.value);
  };

  const [practiceEnd, setpracticeEnd] = useState("");
  const handleInputpracticeEnd= (e: any) => {
    setpracticeEnd(e.target.value);
  };

  const [universityHeadName, setuniversityHeadName] = useState("");
  const handleInputuniversityHeadName= (e: any) => {
    setuniversityHeadName(e.target.value);
  };

  const [universityHeadPatronymic, setuniversityHeadPatronymic] = useState("");
  const handleInputuniversityHeadPatronymic= (e: any) => {
    setuniversityHeadPatronymic(e.target.value);
  };

  const [universityHeadLastName, setuniversityHeadLastName] = useState("");
  const handleInputuniversityHeadLastName= (e: any) => {
    setuniversityHeadLastName(e.target.value);
  };

  const [roomName, setroomName] = useState("");
  const handleInputroomName= (e: any) => {
    setroomName(e.target.value);
  };

  const [technicalMeans, settechnicalMeans] = useState("");
  const handleInputtechnicalMeans= (e: any) => {
    settechnicalMeans(e.target.value);
  };

  const postCreateDocument = async () => {
      accountApi.createDocument(
        fullnameCompany,
        contactJobName, 
        contactName, 
        contactLastname, 
        contactPatronymic, 
        studyProgramName, 
        fullnameCompany, 
        companyAddress, 
        studyCourseName, 
        practiceTypeName, 
        practiceKindName, 
        1,
        profileUserInfo.surname,
        profileUserInfo.name,
        profileUserInfo.patronymic,
        3,
        profileUserInfo.group || '430-4',
        practiceStart,
        practiceEnd,
        universityHeadName,
        universityHeadPatronymic,
        universityHeadLastName,
        roomName,
        technicalMeans
        )
      .then((res) => {
        console.log(res)
        setModalMessage('Заполнение выполнено!')
        statusAuth.open()
      })
      .catch((error) => {
        console.log(error)
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

  return (
    <>
    <StatusAuth statusAuth={statusAuth} message={modalMessage} />
    <div className="bg-gray-main h-screen w-full grid grid-cols-12 gap-4 px-4">
        <div className="col-start-1 col-span-3 bg-white my-[30%] rounded-lg p-4 flex flex-col h-max">
          <p className="text-center text-lg font-bold">Список шаблонов документов</p>
          <div className="flex flex-col mt-4">
            <div onClick={() => setTypeDocument(1)} className={`hover:text-blue-active cursor-pointer ${typeDocument === 1cd && 'text-blue-active'}`}>
              - Шаблон договора о практической подготовке обучающихся в форме практики
            </div>
            <div onClick={() => {}} className="text-gray-300 mt-2">
              - Шаблон о практической подготовке в форме практики
            </div>
            <div onClick={() => {}} className="text-gray-300 mt-2">
              - Шаблон заявления на прохождения практики
            </div>
          </div>
        </div>
        <div className="col-start-4 col-span-12 bg-white my-[9.2%] rounded-lg p-4 flex flex-col h-max">
          {typeDocument === 0 ? (
            <p className="text-center text-lg font-bold">Выберите шаблон документа для заполнения!</p>
          ) : typeDocument === 1 && (
            <div className="flex flex-col">
              {/* Наименование компании */}
              <div className="flex items-center">
                  <p className="whitespace-nowrap text-lg">
                    Наименование компании:{" "}
                  </p>
                  <input
                    value={fullnameCompany}
                    onChange={handleInputFullnameCompany}
                    placeholder="ООО 'ПРОМ-ТОРГ'"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Должность руководителя практики от профильной организации */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Должность руководителя практики от профильной организации:{" "}
                  </p>
                  <input
                    value={contactJobName}
                    onChange={handleInputcontactJobName}
                    placeholder="Генеральный Директор"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Имя руководителя практики от профильной организации */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Имя руководителя практики от профильной организации:{" "}
                  </p>
                  <input
                    value={contactName}
                    onChange={handleInputcontactName}
                    placeholder="Иван"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Фамилия руководителя практики от профильной организации */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Фамилия руководителя практики от профильной организации:{" "}
                  </p>
                  <input
                    value={contactLastname}
                    onChange={handleInputcontactLastname}
                    placeholder="Иванов"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Отчество руководителя практики от профильной организации */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Отчество руководителя практики от профильной организации:{" "}
                  </p>
                  <input
                    value={contactPatronymic}
                    onChange={handleInputcontactPatronymic}
                    placeholder="Иванович"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Направление подготовки */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Направление подготовки:{" "}
                  </p>
                  <input
                    value={studyProgramName}
                    onChange={handleInputstudyProgramName}
                    placeholder="09.03.01 - 'Информатика и вычислительная техника' "
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Адрес предприятия */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Адрес предприятия:{" "}
                  </p>
                  <input
                    value={companyAddress}
                    onChange={handleInputcompanyAddress}
                    placeholder="119415, город Москва, пр-кт Вернадского, д, 41 стр 1, ком. 2"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Профиль обучения */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Профиль обучения:{" "}
                  </p>
                  <input
                    value={studyCourseName}
                    onChange={handleInputstudyCourseName}
                    placeholder="Программное обеспечение средств вычислительной техники и автоматизированных систем"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Вид практики */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Вид практики:{" "}
                  </p>
                  <input
                    value={practiceTypeName}
                    onChange={handleInputpracticeTypeName}
                    placeholder="Производственная практика"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Тип практики */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Тип практики:{" "}
                  </p>
                  <input
                    value={practiceKindName}
                    onChange={handleInputpracticeKindName}
                    placeholder="Научно-исследовательская работа"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Дата начала практики */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Дата начала практики:{" "}
                  </p>
                  <input
                    value={practiceStart}
                    onChange={handleInputpracticeStart}
                    placeholder="06.02.2023г."
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Дата окончания практики */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                    Дата окончания практики:{" "}
                  </p>
                  <input
                    value={practiceEnd}
                    onChange={handleInputpracticeEnd}
                    placeholder="04.03.2023г."
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Имя руководителя практики от Университета*/}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                  Имя руководителя практики от Университета:{" "}
                  </p>
                  <input
                    value={universityHeadName}
                    onChange={handleInputuniversityHeadName}
                    placeholder="Александр"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Отчество руководителя практики от Университета*/}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                  Отчество руководителя практики от Университета:{" "}
                  </p>
                  <input
                    value={universityHeadPatronymic}
                    onChange={handleInputuniversityHeadPatronymic}
                    placeholder="Александрович"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Фамилия руководителя практики от Университета*/}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                  Фамилия руководителя практики от Университета:{" "}
                  </p>
                  <input
                    value={universityHeadLastName}
                    onChange={handleInputuniversityHeadLastName}
                    placeholder="Федотов"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Наименование помещения*/}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                  Наименование помещения:{" "}
                  </p>
                  <input
                    value={roomName}
                    onChange={handleInputroomName}
                    placeholder="Офис"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              {/* Перечень материально-технических средств и программного обеспечения в помещении */}
              <div className="flex items-center mt-2">
                  <p className="whitespace-nowrap text-lg">
                  Перечень материально-технических средств и программного обеспечения в помещении:{" "}
                  </p>
                  <input
                    value={technicalMeans}
                    onChange={handleInputtechnicalMeans}
                    placeholder="Стол, стул"
                    type="text"
                    className="rounded-lg bg-transparent outline-none ml-2 p-1 text-base text-black placeholder:text-menu-text-gray border font-normal w-full"
                  />
              </div>

              <button 
                onClick={() => postCreateDocument()}
                disabled={!technicalMeans || !roomName}
                className="mt-4 w-max border-2 self-center p-2 rounded-md hover:border-blue-active disabled:border-gray-500 disabled:bg-gray-500">
                  Заполнить шаблон
                </button>
            </div>
          )}
        </div>
    </div>
    </>
  );
};

export default Documents;
