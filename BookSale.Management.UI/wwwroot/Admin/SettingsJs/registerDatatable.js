function registerDatatable(elementName, columns, urlApi) {
    $(elementName).DataTable({
        paging: true,
        processing: true,
        serverSide: true,
        columns: columns,
        language: {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Hiển thị _MENU_ bản ghi",
            "sZeroRecords": "Không tìm thấy bản ghi nào",
            "sInfo": "Hiển thị từ _START_ đến _END_ của _TOTAL_ bản ghi",
            "sInfoEmpty": "Hiển thị từ 0 đến 0 của 0 bản ghi",
            "sInfoFiltered": "(được lọc từ _MAX_ bản ghi)",
            "sInfoPostFix": "",
            "sSearch": "Tìm kiếm:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Đầu tiên",
                "sPrevious": "Trước",
                "sNext": "Tiếp",
                "sLast": "Cuối cùng"
            }
        },
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json',
            dataSrc: function (json) {
                // Cập nhật thông tin phân trang cho DataTable
                json.recordsTotal = json.RecordsTotal;
                json.recordsFiltered = json.RecordsFiltered;
                return json.Data;
            }
        }
    });
}