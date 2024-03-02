function Delete(url) {
    Swal.fire({
        title: 'Bạn có chắc sẽ xoá chứ ?',
        text: "Không thể hoàn tác lại dữ liệu !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Không',
        confirmButtonText: 'Tiếp tục xoá'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                dataType: 'json',
                method: 'POST',
                success: function (data) {
                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": true,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": false,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    };
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