function deleteOrganization(organizationRow) {
    let organizationId = $(organizationRow).attr("name");

    $.ajax({
        url: "/Organizations/DeleteOrganization/" + organizationId,
        type: 'DELETE',
        contentType: 'application/json',
        success: function (response) {
            if (response.data) {
                removeOrganizationFromOrganizationTable(organizationRow);
            }
        },
        error: function (error) {
            console.log("Ошибка");
        }
    });
}

function removeOrganizationFromOrganizationTable(organizationRow) {
    $(organizationRow).remove();
}