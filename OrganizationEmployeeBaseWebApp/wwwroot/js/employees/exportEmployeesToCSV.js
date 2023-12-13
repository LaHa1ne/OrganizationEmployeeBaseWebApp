function exportEmployeesToCSV() {
    let organizationId = $('#organizationId').text();

    $.ajax({
        url: '/Employees/ExportOrganizationEmployeesToCSV/' + organizationId,
        type: 'POST',
        success: function (data) {
            var blob = new Blob([data], { type: 'text/csv' });
            var url = window.URL.createObjectURL(blob);

            var a = document.createElement('a');
            a.href = url;
            a.download = 'employees.csv'; 
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
            document.body.removeChild(a);
        },
        error: function (error) {
            console.error('Ошибка', error);
        }
    });
}