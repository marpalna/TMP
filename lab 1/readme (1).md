# Диаграмма испольщования
```
@startuml
left to right direction

actor Пользователь
actor Администратор

usecase "Регистрация" as UC1
usecase "Вход в систему" as UC2
usecase "Выход из системы" as UC3
usecase "Редактирование профиля" as UC4
usecase "Изменение номера телефона" as UC5
usecase "Изменение имя пользователя" as UC6
usecase "Отправка сообщения" as UC7
usecase "Отправить текст" as UC8
usecase "Отправить фото" as UC9
usecase "Отправка кода подтверждения" as UC10
usecase "Бан пользователя" as UC11


Пользователь --> UC1
Пользователь --> UC2
UC2 ..> UC3 : extends
Пользователь --> UC4
UC4 ..> UC5 : include
UC4 ..> UC6 : include
Пользователь --> UC7
UC7 ..> UC8 : include
UC7 ..> UC9 : include
UC1 --> Администратор
Администратор --> UC10
Администратор --> UC11
@enduml
```
![1](https://user-images.githubusercontent.com/90778174/232498215-61e08ec8-a08f-4974-be52-6581e990e2a4.png)

# Диаграмма классов
```
@startuml
left to right direction

class Профиль {
  +name : string
  -email : string
  -phone : string
  +sendMessage() : void
}
class Пользователь {
  +regdate : date
  +chatlist : array
}
class Администратор {
  +banUser(user) : void
  +sendConfirmCode(sendTo) : void
  +createProfile() : void
}
class Сервер {
  -address : string
  +getMessages() : string
  +getUserList() : array
}
class Сообщение {
  +text : string
  +timest : timestamp
  +sender : user
  +chat : chat
  +sendNotification() : void
}

Профиль --> Пользователь : extends
Профиль --> Администратор : extends
Администратор --> Пользователь : регистрирует
Пользователь --> Сообщение : отправляет
Сервер --> Сообщение : записывает в базу
@enduml
```

![2](https://user-images.githubusercontent.com/90778174/232498252-6f2efa36-29c9-44c4-bcaa-2957a3558591.png)
