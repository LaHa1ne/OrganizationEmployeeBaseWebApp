## Веб-приложение для управления базой сотрудников организаций
[Файл с заданием](Task.md)  
Используемый стек:  
Back: ASP NET Core 6, EF Core + MSSQL  
Front: HTML, Bootstrap, CSS, JS  

Для упрощения переносимости и сборки решения внедрен Docker с поддержкой сборки с использованием docker-compose
Для обычной сборки предварительно необходимо изменить строку подключения к БД:
`\OrganizationEmployeeBaseWebApp\OrganizationEmployeeBaseWebApp\appsettings.{environment}.json`

Скрипты создания и заполнения БД данными находятся в: `\OrganizationEmployeeBaseWebApp\OrganizationEmployeeBaseWebApp.DataAccessLayer\Migrations`

#### Реализованный функционал
Для организаций:
- Создание/редактирование/удаление (вместе с сотрудниками) организаций с использованием пользовательского интерфейса
- Загрузка новых организаций (как с сотрудниками, так и без) из .csv файлов
- Выгрузка организаций (с сотрудниками) в .csv файл

Для сотрудников:
- Создание/редактирование/удаление сотрудников с использованием пользовательского интерфейса
- Загрузка новых сотрудников в выбранную организацию из .csv файлов
- Выгрузка сотрудников выбранной организации в .csv файл

#### Работа с .csv файлами
Для упрощения работы  с .csv файлами используется библиотека **CsvHelper**
Примеры загружаемых и выгружаемых файлов расположены в 
`\OrganizationEmployeeBaseWebApp\OrganizationEmployeeBaseWebApp\ImportExamples\` и 
`\OrganizationEmployeeBaseWebApp\OrganizationEmployeeBaseWebApp\ExportExamples\`

В случае загрузки данных предварительно указанные значения *OrganizationId* и *EmployeeId* заменяются значениями, установленными самой БД
