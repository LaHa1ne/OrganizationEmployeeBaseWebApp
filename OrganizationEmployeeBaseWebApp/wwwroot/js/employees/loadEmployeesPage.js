$(document).ready(function () {
    let employeeTable = $('.employees-info-table');

    $(employeeTable).on('click', '.edit-button', function () {
        var employeeRow = $(this).closest('tr');
        showEditEmployeeForm(employeeRow);
    });

    $(employeeTable).on('click', '.delete-button', function () {
        var employeeRow = $(this).closest('tr');
        deleteEmployee(employeeRow);
    });

    $('.load-employees-button').on('click', function () {
        loadEmployeesFromCSV();
    });

    $('.export-employees-button').on('click', function () {
        exportEmployeesToCSV();
    });
});