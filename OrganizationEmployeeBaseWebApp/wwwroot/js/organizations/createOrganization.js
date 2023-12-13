$(document).ready(function () {
    $('.createOrganizationForm').submit(function (e) {
        e.preventDefault();
        createOrganization(this);
    });
});

function createOrganization(createOrganizationForm) {
    var formData = $(createOrganizationForm).serialize();

    $.ajax({
        url: $(createOrganizationForm).attr('action'),
        type: $(createOrganizationForm).attr('method'),
        data: formData,
        success: function (response) {
            if (response.statusCode == 201) {
                $('.createOrganizationForm')[0].reset();
                $('#createOrganizationModal').modal('hide');
                addOrganizationToOrganizationTable(response.data);
            }
            else {
                console.log(response);
                $('#createOrganizationModal').find('.modal-body').html(response);
                $('.createOrganizationForm').submit(function (e) {
                    e.preventDefault();
                    createOrganization(this);
                });
            }
        },
        error: function (error) {
            console.error('Ошибка', error);
        }
    });
}

function addOrganizationToOrganizationTable(data) {
    console.log(data);
    let empty_organization = $('.organizations-info-table').find('tr[name="empty_organization"]').clone(true);
    for (let organization_attr_name in data) {
        let td = $(empty_organization).find('td.' + organization_attr_name);
        $(td).text(data[organization_attr_name]);
    }
    $(empty_organization).find('.employees-button').attr('href', '/Employees/Employees/' + data.organizationId);
    $(empty_organization).attr('name', data.organizationId);
    $(empty_organization).css('display', 'table-row');
    $('.organizations-info-table').append(empty_organization);
}