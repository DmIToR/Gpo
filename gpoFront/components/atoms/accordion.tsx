import { useState } from "react";

interface AccordeonProps {
  children: any;
  name: string;
  className?: string;
}

export const Accordeon = ({ children, name, className }: AccordeonProps) => {
  const [isOpenDropDown, setIsOpenDropDown] = useState(false);
  const [showLangs, setShowLangs] = useState(false);

  const toggleLangBar = () => {
    if (!isOpenDropDown) {
      setIsOpenDropDown(true);
      setTimeout(() => setShowLangs(true), 500);
    } else if (isOpenDropDown) {
      setShowLangs(false);
      setTimeout(() => setIsOpenDropDown(false), 500);
    }
  };

  return (
    <div className={`flex flex-col border-2 rounded-md p-2 ${className}`}>
      <div onClick={toggleLangBar} className="flex justify-between items-center cursor-pointer">
        <p className="text-xl w-full text-center ml-3">{name}</p>
        <div className={`cursor-pointer ${isOpenDropDown ? 'rotate-180' : 'rotate-0'} transition-all duration-500 mr-2`}>
            &darr;
        </div>
      </div>
      <div
        className={`${
          isOpenDropDown ? "h-max" : "h-0 overflow-hidden"
        } transition-all duration-500 mt-2`}
      >
        <div
          className={`${
            showLangs
              ? "showContentNavBar opacity-100"
              : "hideContentNavBar opacity-0"
          } transition-all duration-500 flex flex-col p-2 border rounded-md`}
        >
          {children}
        </div>
      </div>
    </div>
  );
};
