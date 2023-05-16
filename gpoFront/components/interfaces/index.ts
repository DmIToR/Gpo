export interface IModal {
  className?: string;
  isOpen: boolean;
  open: (arg0: string) => void;
  close: () => void;
  customClasses?: boolean;
  children: any;
  onSideClick?: () => void;
}

export interface IStatusAuth {
  statusAuth: any;
  message: string;
}

export enum ProfileItems {
  group = "Группа",
  department = "Кафедра",
  post = "Должность",
  name = "Имя",
  surname = "Фамилия",
  patronymic = "Отчество",
  username = 'Логин',
  email = 'Почта'
}

export interface IProfileInfoItem {
  name: keyof typeof ProfileItems;
  value: string;
}
