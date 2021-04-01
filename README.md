# TestMessageHub
Тестовый проект "общалка" трех компаний - Пума, Адидас да Найк.
Реализовано получение сообщений и отправка новых.

Пример запроса получения: GET http://localhost:5000/Messages/?companyName=ADIDAS&fromDate=2020-01-01 23:59&toDate=2022-01-01 23:59&read=false

Пример запроса отправки нового: POST http://localhost:5000/Messages/

{
        "header": "Первое сообщение для пумы",
        "content": "Тут текст сообщения для пумы от адидас",
        "from": "ADIDAS",
        "to": "PUMA",
        "sendDate": "2021-03-31T18:08:31.503086",
        "read": true
}
