$(document).ready(function () {
    let organizationTable = $('.organizations-info-table');

    $(organizationTable).on('click', '.edit-button', function () {
        var organizationRow = $(this).closest('tr');
        showEditOrganizationForm(organizationRow);
    });

    $(organizationTable).on('click', '.delete-button', function () {
        var organizationRow = $(this).closest('tr');
        deleteOrganization(organizationRow);
    });

    $('.load-organizations-button').on('click', function () {
        loadOrganizationsFromCSV();
    });

    $('.export-organizations-button').on('click', function () {
        exportOrganizationsToCSV();
    });
});