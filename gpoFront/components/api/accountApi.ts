import { accountData, accountSignIn } from "../interfaces/accountDto";
import ApiService from "../services/ApiService";

class AccountApi extends ApiService {
    constructor() {
        super(`Account`);
    }
    
    signIn(userName: string, password: string): Promise<accountSignIn>  {
        const data: accountData = {
            username: userName,
            password: password
        }
        return this.post(`SignIn`, data, '')
    }

    createDocument(
        fullnameCompany: string,
        contactJobName: string,
        contactName: string,
        contactLastname: string,
        contactPatronymic: string,
        studyProgramName: string,
        shortCompanyName: string, //наименование компании вставить
        companyAddress: string,
        studyCourseName: string,
        practiceTypeName: string,
        practiceKindName: string,
        studentsCount: number, //1
        studentLastName: string, //уже есть
        studentFirstName: string, //уже есть
        studentPatronymic: string, //уже есть
        studentCourse: number, //3
        studentGroup: string, //уже есть
        practiceStart: string,
        practiceEnd: string,
        universityHeadName: string,
        universityHeadPatronymic: string,
        universityHeadLastName: string,
        roomName: string,
        technicalMeans: string
    ) {
        const data = {
        fullnameCompany,
        contactJobName,
        contactName,
        contactLastname,
        contactPatronymic,
        studyProgramName,
        shortCompanyName,
        companyAddress,
        studyCourseName,
        practiceTypeName,
        practiceKindName,
        studentsCount,
        studentLastName,
        studentFirstName,
        studentPatronymic,
        studentCourse,
        studentGroup,
        practiceStart,
        practiceEnd,
        universityHeadName,
        universityHeadPatronymic,
        universityHeadLastName,
        roomName,
        technicalMeans
        }
        console.log(data)
        return this.post(`Tools/CreateDocument`, data, '')
    }
}

export default new AccountApi();
