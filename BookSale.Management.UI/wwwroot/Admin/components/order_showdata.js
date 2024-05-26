const elementName = "#tbl-order";

(function () {
    const columns = [
        {
            data: 'Id', name: 'Id', width: '150px', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a class="btn-edit btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a onClick=Delete('/admin/order/delete/${key}') class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                            &nbsp
                            <a class="btn-dowload btn btn-icon btn-info btn-sm mr-2"><i class="far fa-arrow-alt-circle-down"></i></a>
                    </span>
                `
            }
        },
        { data: 'Fullname', name: 'Fullname', width: "300px" },
        { data: 'Code', name: 'Code', width: "150px" },
        {
            data: 'CreatedOn', name: 'CreatedOn', width: "150px", render: function (data) {
                return `<div class="text-left">${moment(data).format("DD/MM/YYYY")}</div>`
            }
        },
        {
            data: 'TotalPrice', name: 'TotalPrice', width: "150px", render: function (data) {
                return `<div class="text-left">${data.toLocaleString('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                })}</div>`
            }
        },
        {
            data: 'PaymentMethod', name: 'PaymentMethod', width: "60px", render: function (data) {
                return `<div class="text-center">${data}</div>`
            } },
        {
            data: 'Status', name: 'Status', width: "60px", render: function (data) {
                return `<div class="text-center">${formatStatus(data)}</div>`
            }
        }
    ];
    const urlApi = "/admin/order/getorderpagination";

    registerDatatable(elementName, columns, urlApi);


    function formatStatus(data) {
        switch (data.toLowerCase()) {
            case 'new':
                return `<div class="text-center status-new">${data}</div>`;
            case 'processing':
                return `<div class="text-center status-processing">${data}</div>`;
            case 'cancel':
                return `<div class="text-center status-cancel">${data}</div>`;
            default:
                return `<div class="text-center status-complete">${data}</div>`;
        }
    }
})();