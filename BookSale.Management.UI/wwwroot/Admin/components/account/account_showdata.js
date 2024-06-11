const elementName = "#tbl-account";

(function () {
    const columns = [
        {
            data: 'Id', name: 'Id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a href="/admin/account/savedata?id=${key}" class="btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a onClick = Delete('/admin/account/delete/${key}') class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                    </span>
                `
            }
        },
        { data: 'Username', name: 'Username', autoWidth: true },
        { data: 'Fullname', name: 'Fullname', autoWidth: true },
        { data: 'Email', name: 'Email', autoWidth: true },
        { data: 'Phone', name: 'Phone', autoWidth: true },
        {
            data: 'IsActive', name: 'IsActive', autoWidth: true, render: function (data) {
                return data ? 'Hoạt động' : 'Khoá';
            }
        }
    ];
    const urlApi = "/admin/account/getaccountpagination";

    registerDatatable(elementName, columns, urlApi)
})();

