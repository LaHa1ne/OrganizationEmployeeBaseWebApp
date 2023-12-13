$(document).ready(function () {
    $('.createEmployeeForm').submit(function (e) {
        e.preventDefault();
        createEmployee(this);
    });
});

function createEmployee(createEmployeeForm) {
    var formData = $(createEmployeeForm).serialize();

    $.ajax({
        url: $(createEmployeeForm).attr('action'),
        type: $(createEmployeeForm).attr('method'),
        data: formData,
        success: function (response) {
            if (response.statusCode == 201) {
                $('.createEmployeeForm')[0].reset();
                $('#createEmployeeModal').modal('hide');
                addEmployeeToEmployeeTable(response.data);
            }
            else {
                console.log(response);
                $('#createEmployeeModal').find('.modal-body').html(response);
                $('.createEmployeeForm').submit(function (e) {
                    e.preventDefault();
                    createEmployee(this);
                });
            }
        },
        error: function (error) {
            console.error('Ошибка', error);
        }
    });
}

function addEmployeeToEmployeeTable(data) {
    console.log(data);
    let empty_employee = $('.employees-info-table').find('tr[name="empty_employee"]').clone(true);
    for (let employee_attr_name in data) {
        let td = $(empty_employee).find('td.' + employee_attr_name);
        $(td).text(data[employee_attr_name]);
    }
    $(empty_employee).attr('name', data.employeeId);
    $(empty_employee).css('display', 'table-row');
    $('.employees-info-table').append(empty_employee);
}