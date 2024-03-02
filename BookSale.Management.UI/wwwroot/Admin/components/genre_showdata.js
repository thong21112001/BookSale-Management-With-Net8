(function () {
    const elementName = "#tbl-genre";
    const columns = [
        {
            data: 'id', name: 'id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a href="/admin/genre/savedata?id=${key}" class="btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
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

    $(document).on('click', '#btn-add', function () {
        $('#genre-modal').modal('show');
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