function deleteEmployee(employeeRow) {
    let employeeId = $(employeeRow).attr("name");

    $.ajax({
        url: "/Employees/DeleteEmployee/" + employeeId,
        type: 'DELETE',
        contentType: 'application/json',
        success: function (response) {
            if (response.data) {
                removeEmployeeFromEmployeeTable(employeeRow);
            }
        },
        error: function (error) {
            console.log("Ошибка", error);
        }
    });
}

function removeEmployeeFromEmployeeTable(employeeRow) {
    $(employeeRow).remove();
}