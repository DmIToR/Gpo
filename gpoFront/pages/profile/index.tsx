import type { NextPage } from "next";
import { useContext, useEffect, useState } from "react";
import { ProfileUserContext } from "../../components/context/profileUserContext";
import { IProfileInfoItem, ProfileItems } from "../../components/interfaces";

const Profile: NextPage = () => {
  const { role, profileUserInfo } = useContext(ProfileUserContext);
  const [info, setInfo] = useState<IProfileInfoItem[]>([]);

  useEffect(() => {
    let tempArray: IProfileInfoItem[] = [];
    Object.entries(profileUserInfo).map((item: any) => {
        if(item[0] !== 'id') tempArray.push(
          {
            name: item[0],
            value: item[1]
          }
        )
    });
    setInfo(tempArray)
  }, [profileUserInfo]);
  return (
    <div className="bg-gray-main h-screen w-full grid grid-cols-12 gap-4">
      <div className="col-start-4 col-span-6 bg-white my-[20%] rounded-lg p-6 flex flex-col h-max">
        <p className="text-center text-2xl pb-1 mb-6 w-max self-center">
          {role}
        </p>
        <div className="flex flex-col">
          {info && info.map((item, index) => (
            <div key={index} className="grid grid-cols-[20%_80%] items-center pb-2">
              <p className="text-xl text-right">{ProfileItems[item.name]}: </p>
              <div className="ml-4 w-max border-2 p-2 rounded-lg text-lg">
                {item.value}
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Profile;
