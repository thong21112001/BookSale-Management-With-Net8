const elementName = "#tbl-book";

(function () {
    const columns = [
        { data: 'GenreName', name: 'GenreName', autoWidth: true },
        { data: 'Code', name: 'Code', autoWidth: true },
        { data: 'Title', name: 'Title', autoWidth: true },
        { data: 'Available', name: 'Available', autoWidth: true },
        {
            data: 'Price', name: 'Price', autoWidth: true, render: function (data) {
                return `<div class="text-left">${data.toLocaleString('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                })}</div>`
            }
        },
        { data: 'Publisher', name: 'Publisher', autoWidth: true },
        { data: 'Author', name: 'Author', autoWidth: true },
        {
            data: 'CreatedOn', name: 'CreatedOn', autoWidth: true, render: function (data){
                return `<div class="text-left">${moment(data).format("DD/MM/YYYY")}</div>`
            }
        },
        {
            data: 'Id', name: 'Id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a href="/admin/book/savedata?id=${key}" class="btn-edit btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a onClick = Delete('/admin/genre/delete/${key}') class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                    </span>
                `
            }
        }
    ];
    const urlApi = "/admin/book/getbookpagination";

    registerDatatable(elementName, columns, urlApi);
})();