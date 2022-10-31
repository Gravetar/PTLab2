# Лабораторная работа 2 по дисциплине "Технологии программирования"
## Постановка задачи
**Цели:**
1. Познакомиться c моделью MVC, ее сущностью и основными фреймворками на ее основе.
2. Разобраться с сущностями «модель», «контроллер», «представление», их функциональным
назначением.
3. Получить навыки разработки веб-приложений с использованием MVC-фреймворков.

### Индивидуальное задание:
**Тип магазина:** Магазин бытовой техники
**Функциональность приложения:** Магазин должен вести учет покупателей и делать накопительную скидку. Величина скидки зависит от общей суммы покупок любых товаров.
### Краткое описание проекта:
Разработанное приложение позволяет авторизироваться и регистрироваться пользователям. У каждого пользователя ведется свой учет и накапливается скидка. По условию задачи не было уточнено сколько дается скидка и за какую общую потраченную сумму она дается, было решено взять абстрактные случайные значения в виде 1% скидки на все товары за каждые потраченные 10.000 рублей в магазине.

**Было разработано:**
* 2 модели: Product, User.
* 3 контроллера: Account, Home, Shop.
* 4 представления: Login, Register, Index(Home), Shop.  

### Используемые языки / библиотеки / технологии
Язык проекта - C#  
Дополнительные библиотеки:
* *EntityFrameworkCore* - Для работы с базой данных
* *AspNetCore.Mvc* - Для работы с MVC
* *Authentication.Cookies* - Для аутентификации на основе куки
* *ComponentModel.DataAnnotationss* - Предоставляет классы атрибутов, используемые для определения метаданных для ASP.NET MVC и элементов управления данными ASPNET.

### Выводы
Было разработано приложение согласно индивидуальному заданию, протестировано разработанное приложение, проблем выявено не было.
**В процессе данной разработки я познакомился с:**
* Моделью MVC, ее сущностью и основными фреймворками на ее основе.
* Сущностями «модель», «контроллер», «представление», их функциональным назначением в модели MVC.  

**Также я получил:**
* Навыки разработки веб-приложений с использованием MVC-фреймворков.
