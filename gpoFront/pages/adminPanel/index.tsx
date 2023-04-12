import type { NextPage } from "next";

const adminPanel: NextPage = () => {
  return (
    <div className="bg-gray-main h-screen w-full grid grid-cols-12 gap-4">
      <div className="col-start-4 col-span-6 bg-white my-[20%] rounded-lg p-6 flex flex-col h-max">
            <p className="text-center text-2xl pb-1 mb-6 w-max self-center">Панель администратора</p>
            
      </div>
    </div>
  );
};

export default adminPanel;
