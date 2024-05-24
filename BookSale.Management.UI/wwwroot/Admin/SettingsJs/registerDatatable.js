function registerDatatable(elementName, columns, urlApi) {
    $(elementName).DataTable({
        paging: true,
        processing: true,
        serverSide: true,
        columns: columns,
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json',
            dataSrc: function (json) {
                console.log('Server Response:', json);
                return json.Data;
            } // Thêm tùy chọn dataSrc ở đây để chỉ định rằng class ResponseDataTable có thuộc tính trả về là Data
        }
    });
}