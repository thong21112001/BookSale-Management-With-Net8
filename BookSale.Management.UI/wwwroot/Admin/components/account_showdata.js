const elementName = "#tbl-account";

(function () {
    const columns = [
        {
            data: 'id', name: 'id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a href="/admin/account/savedata?id=${key}" class="btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a onClick = Delete('/admin/account/delete/${key}') class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                    </span>
                `
            }
        },
        { data: 'username', name: 'username', autoWidth: true },
        { data: 'fullname', name: 'fullname', autoWidth: true },
        { data: 'email', name: 'email', autoWidth: true },
        { data: 'phone', name: 'phone', autoWidth: true },
        {
            data: 'isActive', name: 'isActive', autoWidth: true, render: function (data) {
                return data ? 'Hoạt động' : 'Khoá';
            }
        }
    ];
    const urlApi = "/admin/account/getaccountpagination";

    registerDatatable(elementName, columns, urlApi)
})();

