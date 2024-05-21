const elementName = "#tbl-order";

(function () {
    const columns = [
        { data: 'Id', name: 'Id', autoWidth: true },
        { data: 'Code', name: 'Code', autoWidth: true },
        {
            data: 'CreatedOn', name: 'CreatedOn', autoWidth: true, render: function (data) {
                return `<div class="text-left">${moment(data).format("DD/MM/YYYY")}</div>`
            }
        },
        { data: 'PaymentMethod', name: 'PaymentMethod', width: "60px" },
        {
            data: 'Status', name: 'Status', width: "60px", render: function (data) {
                return `<div class="text-center">${formatStatus(data)}</div>`
            }
        },
        { data: 'Fullname', name: 'Fullname', autoWidth: true },
        {
            data: 'TotalPrice', name: 'TotalPrice', autoWidth: true, render: function (data) {
                return `<div class="text-left">${data.toLocaleString('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                })}</div>`
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