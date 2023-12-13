function showEditEmployeeForm(employeeRow) {
    let editEmployeeModalForm = $('#editEmployeeModal');
    let editEmployeeForm = $(editEmployeeModalForm).find('.editEmployeeForm');
    let employeeId = $(employeeRow).find('td.employeeId').text();
    let firstName = $(employeeRow).find('td.firstName').text();
    let lastName = $(employeeRow).find('td.lastName').text();
    let patronymic = $(employeeRow).find('td.patronymic').text();
    let dateOfBirth = $(employeeRow).find('td.dateOfBirth').text();
    let passportSeries = $(employeeRow).find('td.passportSeries').text();
    let passportNumber = $(employeeRow).find('td.passportNumber').text();

    $(editEmployeeForm).find('input[name="employeeId"]').val(employeeId);
    $(editEmployeeForm).find('input[name="firstName"]').val(firstName);
    $(editEmployeeForm).find('input[name="lastName"]').val(lastName);
    $(editEmployeeForm).find('input[name="patronymic"]').val(patronymic);
    $(editEmployeeForm).find('input[name="dateOfBirth"]').val(formatDate(dateOfBirth));
    $(editEmployeeForm).find('input[name="passportSeries"]').val(passportSeries);
    $(editEmployeeForm).find('input[name="passportNumber"]').val(passportNumber);

    editEmployeeModalForm.modal("show");
}

$(document).ready(function () {
    $('.editEmployeeForm').submit(function (e) {
        e.preventDefault();
        editEmployee(this);
    });
});

function editEmployee(editEmployeeForm) {
    var formData = $(editEmployeeForm).serialize();

    $.ajax({
        url: $(editEmployeeForm).attr('action'),
        type: $(editEmployeeForm).attr('method'),
        data: formData,
        success: function (response) {
            if (response.statusCode == 200) {
                $('.editEmployeeForm')[0].reset();
                $('#editEmployeeModal').modal('hide');
                editEmployeeInEmployeeTable(response.data);
            }
            else {
                $('#editEmployeeModal .modal-body').html(response);
                $('.editEmployeeForm').submit(function (e) {
                    e.preventDefault();
                    editEmployee(this);
                });
            }
        },
        error: function (error) {
            console.error('Ошибка', error);
        }
    });
}

function editEmployeeInEmployeeTable(data) {
    console.log(data);
    let edited_employee = $('.employees-info-table').find('tr[name="' + data.employeeId + '"]');
    for (let employee_attr_name in data) {
        let td = $(edited_employee).find('td.' + employee_attr_name);
        $(td).text(data[employee_attr_name]);
    }
}