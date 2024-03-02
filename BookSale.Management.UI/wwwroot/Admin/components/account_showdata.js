const elementName = "#tbl-account";

(function () {
    const columns = [
        {
            data: 'id', name: 'id', width: '100', render: function (key) {
                return `
                    <span data-key="${key}">
                            <a href="/admin/account/savedata?id=${key}" class="btn btn-icon btn-warning btn-sm mr-2"><i class="fas fa-pencil-alt"></i></a>
                            &nbsp
							<a onClick = Delete('/admin/account/delete/${key}') class="btn btn-icon btn-danger btn-sm mr-2"><i class="far fa-trash-alt"></i></a>
                    </span>
                `
            }
        },
        { data: 'username', name: 'username', autoWidth: true },
        { data: 'fullname', name: 'fullname', autoWidth: true },
        { data: 'email', name: 'email', autoWidth: true },
        { data: 'phone', name: 'phone', autoWidth: true },
        { data: 'isActive', name: 'isActive', autoWidth: true }
    ];
    const urlApi = "/admin/account/getaccountpagination";

    registerDatatable(elementName, columns, urlApi)
})();

/*Hàm khi bấm nút xóa*/
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                dataType: 'json',
                method: 'POST',
                success: function (data) {
                    if (!data) {
                        toastr["error"]('Lỗi khi xoá !!!');
                        return;
                    }
                    $(elementName).DataTable().ajax.reload();
                    toastr["success"]('Xoá thành công !!!');
                }
            })
        }
    })
}