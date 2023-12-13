function loadOrganizationsFromCSV() {
    let fileInput = $('#fileWithLoadedOrganizationsInput')[0];

    if (fileInput.files.length > 0 && fileInput.files[0].name.endsWith('.csv')) {
        let formData = new FormData();
        formData.append('file', fileInput.files[0]);

        $.ajax({
            url: '/Organizations/LoadOrganizationsFromCSV/',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.statusCode == 200) {
                    for (let organization of response.data) {
                        addOrganizationToOrganizationTable(organization);
                    }
                }
            },
            error: function (error) {
                // Обработка ошибки
                console.error('Ошибка', error);
            },
            complete: function () {
                // Очистка поля для загрузки файла после успешного запроса
                $('#fileWithLoadedOrganizationsInput').val('');
            }
        });
    } else {
        console.log('Выберите файл CSV для загрузки.');
        $('#fileWithLoadedOrganizationsInput').val('');
    }
}