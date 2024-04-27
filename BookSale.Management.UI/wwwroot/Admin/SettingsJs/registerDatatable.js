function registerDatatable(elementName, columns, urlApi) {
    $(elementName).DataTable({
        scrollX: true,
        scrollY: 300,
        processing: true,
        serverSide: true,
        columns: columns,
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json',
            dataSrc: 'Data' // Thêm tùy chọn dataSrc ở đây để chỉ định rằng class ResponseDataTable có thuộc tính trả về là Data
        }
    });
}