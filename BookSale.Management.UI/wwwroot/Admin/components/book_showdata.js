const elementName = "#tbl-book";

(function () {
    const columns = [
        {
            data: 'id', name: 'id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a class="btn-edit btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a onClick = Delete('/admin/genre/delete/${key}') class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                    </span>
                `
            }
        },
        { data: 'genreName', name: 'genreName', autoWidth: true },
        { data: 'code', name: 'code', autoWidth: true },
        { data: 'title', name: 'title', autoWidth: true },
        { data: 'available', name: 'available', autoWidth: true },
        { data: 'cost', name: 'cost', autoWidth: true },
        { data: 'publisher', name: 'publisher', autoWidth: true },
        { data: 'author', name: 'author', autoWidth: true },
        { data: 'createdOn', name: 'createdOn', autoWidth: true },
        {
            data: 'isActive', name: 'isActive', autoWidth: true, render: function (data) {
                return data ? 'Hiển thị' : 'Không';
            }
        }
    ];
    const urlApi = "/admin/book/getbookpagination";

    registerDatatable(elementName, columns, urlApi)

    //Khi click vào add thì toàn bộ đưa về mặc định
    //$(document).on('click', '#btn-add', function () {
    //    $('#Id').val(0);
    //    $('#Name').val('');
    //    $('#Description').val('');
    //    $('#isActive').prop('checked', true);
    //    $('#genre-modal').modal('show');
    //});

    ////Khi click action edit thì map dữ liệu vào form
    //$(document).on('click', '.btn-edit', function () {
    //    const key = $(this).closest('span').data('key');

    //    $.ajax({
    //        url: `/admin/genre/getbyid?id=${key}`,
    //        method: "GET",
    //        success: function (response) {
    //            mapObjectToModalView(response);
    //            $('#genre-modal').modal('show');
    //        }
    //    })
    //});

    //Dùng để get data truyền vào controller tiến hành thêm hoặc sửa
    //$('#formGenre').submit(function (e) {
    //    e.preventDefault();

    //    const formData = $(this).serialize();//Get toàn bộ trong form input trả về json data

    //    $.ajax({
    //        url: $(this).attr('action'),
    //        method: $(this).attr('method'),
    //        data: formData,
    //        success: function (response) {
    //            if (response.status === "success") {
    //                $(elementName).DataTable().ajax.reload();
    //                showToastAllPage("success", response.message);
    //                $('#genre-modal').modal('hide');
    //            } else if (response.status === "info") {
    //                $(elementName).DataTable().ajax.reload();
    //                showToastAllPage("info", response.message);
    //                $('#genre-modal').modal('hide');
    //            } else {
    //                showToastAllPage("warning", response.message);
    //                $('#genre-modal').modal('hide');
    //            }
    //        }
    //    })
    //});
})();