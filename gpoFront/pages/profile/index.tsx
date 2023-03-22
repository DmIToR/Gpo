import type { NextPage } from "next";

const infoAboutUser : {name: string, lastName: string, fatherName: string, mail: string} = {
    name: 'Александр',
    lastName: 'Александров',
    fatherName: 'Александрович',
    mail: 'petrov2002@mail.ru'
}

const Profile: NextPage = () => {
  return (
    <div className="bg-gray-main h-screen w-full grid grid-cols-12 gap-4">
      <div className="col-start-4 col-span-6 bg-white my-[20%] rounded-lg p-6 flex flex-col h-max">
            <p className="text-center text-2xl pb-1 mb-6 w-max self-center">Информация о пользователе</p>
            <div className="flex flex-col">
                <div className="grid grid-cols-[13%_87%] items-center pb-2">
                    <p className="text-xl text-right">Имя: </p>
                    <div className="ml-4 w-max border-2 p-2 rounded-lg text-lg">{infoAboutUser.name}</div>
                </div>
                <div className="grid grid-cols-[13%_87%] items-center pb-2">
                    <p className="text-xl text-right">Фамилия: </p>
                    <div className="ml-4 w-max border-2 p-2 rounded-lg text-lg">{infoAboutUser.lastName}</div>
                </div>
                <div className="grid grid-cols-[13%_87%] items-center pb-2">
                    <p className="text-xl text-right">Отчество: </p>
                    <div className="ml-4 w-max border-2 p-2 rounded-lg text-lg">{infoAboutUser.fatherName}</div>
                </div>
                <div className="grid grid-cols-[13%_87%] items-center">
                    <p className="text-xl text-right">E-mail: </p>
                    <div className="ml-4 w-max border-2 p-2 rounded-lg text-lg">{infoAboutUser.mail}</div>
                </div>
            </div>
      </div>
    </div>
  );
};

export default Profile;
