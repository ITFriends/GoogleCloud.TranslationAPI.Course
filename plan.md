# План курса "Google Cloud Translation API"

**Цель: создание фонового сервиса на .NET для перевода записей в БД с помощью Google Cloud Translation API**

## Урок 1
- Создание проекта в Google Cloud
- Включение Cloud Translation API
- Создание репозитория на GitHub

## Урок 2
- Локальная аутентификация
- Простые REST запросы Cloud Translation API
- Создаем два .NET проекта для Basic и Advanced

## Урок 3
- Модель базы данных: класс Record, класс Translation
- Контекст 
- Контроллер для добавления новых записей в БД - RecordsController
- Контроллер для считывания переводов - TranslationsController
- Postman

## Урок 4
- Создадим BackgroundService, который будет периодически выполнять задачу
- Подключим логирование
- Подключим контекст базы данных к сервису
- Напишем метод для обращения к Google Translation API Advanced
- Осуществим сохранение перевода в БД
- Протестируем работу с помощью Postman