function loadEmployeesFromCSV() {
    let fileInput = $('#fileWithLoadedEmployeesInput')[0];

    if (fileInput.files.length > 0 && fileInput.files[0].name.endsWith('.csv')) {
        let formData = new FormData();
        formData.append('file', fileInput.files[0]);
        let organizationId = $('#organizationId').text();
        formData.append('organizationId', organizationId);

        $.ajax({
            url: '/Employees/LoadEmployeesFromCSV/',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.statusCode == 200) {
                    for (let employee of response.data) {
                        addEmployeeToEmployeeTable(employee);
                    }
                }
            },
            error: function (error) {
                // Обработка ошибки
                console.error('Ошибка', error);
            },
            complete: function () {
                // Очистка поля для загрузки файла после успешного запроса
                $('#fileWithLoadedEmployeesInput').val('');
            }
        });
    } else {
        console.log('Выберите файл CSV для загрузки.');
        $('#fileWithLoadedEmployeesInput').val('');
    }
}