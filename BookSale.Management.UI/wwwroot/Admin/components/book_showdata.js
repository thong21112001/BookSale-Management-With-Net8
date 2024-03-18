﻿const elementName = "#tbl-book";

(function () {
    const columns = [
        { data: 'genreName', name: 'genreName', autoWidth: true },
        { data: 'code', name: 'code', autoWidth: true },
        { data: 'title', name: 'title', autoWidth: true },
        { data: 'available', name: 'available', autoWidth: true },
        {
            data: 'price', name: 'price', autoWidth: true, render: function (data) {
                return `<div class="text-left">${data.toLocaleString('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                })}</div>`
            }
        },
        { data: 'publisher', name: 'publisher', autoWidth: true },
        { data: 'author', name: 'author', autoWidth: true },
        {
            data: 'createdOn', name: 'createdOn', autoWidth: true, render: function (data){
                return `<div class="text-left">${moment(data).format("DD/MM/YYYY")}</div>`
            }
        },
        {
            data: 'id', name: 'id', width: '100', render: function (key) {
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