(function () {
    const elementName = "#tbl-account";
    const columns = [
        {
            data: 'id', name: 'id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a href="/admin/account/savedata?id=${key}" class="btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a href="#" class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                    </span>
                `
            }
        },
        { data: 'username', name: 'username', autoWidth: true },
        { data: 'fullname', name: 'fullname', autoWidth: true },
        { data: 'email', name: 'email', autoWidth: true },
        { data: 'phone', name: 'phone', autoWidth: true },
        { data: 'isActive', name: 'isActive', autoWidth: true }
    ];
    const urlApi = "/admin/account/getaccountpagination";

    registerDatatable(elementName, columns, urlApi)

    $(document).on('click', '.btn-danger', function () {
        const key = $(this).closest('span').data('key');

        $.ajax({
            url: `/admin/account/delete/${key}`,
            dataType: 'json',
            method: 'POST',
            success: function (response) {
                if (!response) {
                    showToast("Error", "Error bug :<");
                    return;
                }
                $(elementName).DataTable().ajax.reload();
                showToast("Success", "Delete successfully!!!");

            }
        })
    });

})();