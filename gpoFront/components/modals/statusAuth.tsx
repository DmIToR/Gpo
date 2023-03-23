import { CloseIcon } from "../icons/close";
import { IStatusAuth } from "../interfaces";
import ModalLayout from "../layout/ModalLayout";

const StatusAuth = ({
    statusAuth,
    message
}: IStatusAuth) => {

    return (
        <ModalLayout {...statusAuth}>
            <div className="bg-white md:w-[568px] w-[343px] rounded-2xl p-8 font-roboto z-50 -mt-16">
                <div className="flex justify-end mb-8 ">
                    <div 
                    onClick={(e) => { statusAuth.close()}}
                    className="text-black hover:text-blue-active cursor-pointer w-5">
                        <CloseIcon />
                    </div>
                </div>
                <div className="mb-8 text-xl text-center">
                    {message}
                </div>
            </div>
        </ModalLayout>
    )
}
export default StatusAuth;