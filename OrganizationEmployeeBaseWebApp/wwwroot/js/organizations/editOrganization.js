function showEditOrganizationForm(organizationRow) {
    let editOrganizationModalForm = $('#editOrganizationModal');
    let editOrganizationForm = $(editOrganizationModalForm).find('.editOrganizationForm');
    let organizationId = $(organizationRow).find('td.organizationId').text();
    let name = $(organizationRow).find('td.name').text();
    let inn = $(organizationRow).find('td.inn').text();
    let legalAddress = $(organizationRow).find('td.legalAddress').text();
    let actualAddress = $(organizationRow).find('td.actualAddress').text();

    $(editOrganizationForm).find('input[name="organizationId"]').val(organizationId);
    $(editOrganizationForm).find('input[name="name"]').val(name);
    $(editOrganizationForm).find('input[name="inn"]').val(inn);
    $(editOrganizationForm).find('input[name="legalAddress"]').val(legalAddress);
    $(editOrganizationForm).find('input[name="actualAddress"]').val(actualAddress);

    editOrganizationModalForm.modal("show");
}

$(document).ready(function () {
    $('.editOrganizationForm').submit(function (e) {
        e.preventDefault();
        editOrganization(this);
    });
});

function editOrganization(editOrganizationForm) {
    var formData = $(editOrganizationForm).serialize();

    $.ajax({
        url: $(editOrganizationForm).attr('action'),
        type: $(editOrganizationForm).attr('method'),
        data: formData,
        success: function (response) {
            if (response.statusCode == 200) {
                $('.editOrganizationForm')[0].reset();
                $('#editOrganizationModal').modal('hide');
                editOrganizationInOrganizationTable(response.data);
            }
            else {
                $('#editOrganizationModal .modal-body').html(response);
                $('.editOrganizationForm').submit(function (e) {
                    e.preventDefault();
                    editOrganization(this);
                });
            }
        },
        error: function (error) {
            console.error('Ошибка', error);
        }
    });
}

function editOrganizationInOrganizationTable(data) {
    console.log(data);
    let edited_organization = $('.organizations-info-table').find('tr[name="' + data.organizationId + '"]');
    for (let organization_attr_name in data) {
        let td = $(edited_organization).find('td.' + organization_attr_name);
        $(td).text(data[organization_attr_name]);
    }
}