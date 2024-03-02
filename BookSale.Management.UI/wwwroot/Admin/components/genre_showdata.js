(function () {
    const elementName = "#tbl-genre";
    const columns = [
        {
            data: 'id', name: 'id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a href="#" class="btn-edit btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a href="#" class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                    </span>
                `
            }
        },
        { data: 'name', name: 'name', autoWidth: true },
        { data: 'description', name: 'description', autoWidth: true },
        { data: 'isActive', name: 'isActive', autoWidth: true }
    ];
    const urlApi = "/admin/genre/getgenrepagination";

    registerDatatable(elementName, columns, urlApi)

    //Khi click vào add thì toàn bộ đưa về mặc định
    $(document).on('click', '#btn-add', function () {
        $('#Id').val(0);
        $('#Name').val('');
        $('#Description').val('');
        $('#isActive').prop('checked', true);
        $('#genre-modal').modal('show');
    });

    //Khi click action edit thì map dữ liệu vào form
    $(document).on('click', '.btn-edit', function () {
        const key = $(this).closest('span').data('key');

        $.ajax({
            url: `/admin/genre/getbyid?id=${key}`,
            method: "GET",
            success: function (response) {
                //console.log(response);//-> chạy debug ở console client  {id: 2, name: 'Book', description: 'BOOK', isActive: true}
                //$('#Id').val(response.id);
                //$('#Name').val(response.name);
                //$('#Description').val(response.description);
                //$('#IsActive').val(response.isActive);
                //--> bỏ qua các cách trên viết hàm ở một js khác sau làm tiện hơn
                mapObjectToModalView(response);
                $('#genre-modal').modal('show');
            }
        })
    });

    $(document).on('click', '.btn-warning', function () {
        $('#genre-modal').modal('show');
    });

    $('#formGenre').submit(function (e) {
        e.preventDefault();

        const formData = $(this).serialize();//Get toàn bộ trong form input trả về json data

        $.ajax({
            url: $(this).attr('action'),
            method: $(this).attr('method'),
            data: formData,
            success: function (response) {
                $(elementName).DataTable().ajax.reload();
                showToast("Success", "Delete successfully!!!");
                $('#genre-modal').modal('hide');
            },
            error: function () {

            }
        })

    });
})();