(function () {
    //Khi click vào add thì toàn bộ đưa về mặc định
    $(document).on('click', '#btn-add', function () {
        $('#address-modal').modal('show');
    });

    //Dùng để get data truyền vào controller tiến hành thêm hoặc sửa
    $('#formAddress').submit(function (e) {
        e.preventDefault();

        const formData = $(this).serialize();//Get toàn bộ trong form input trả về json data

        $.ajax({
            url: $(this).attr('action'),
            method: $(this).attr('method'),
            data: formData,
            success: function (response) {
                if (response.status === "success") {
                    showToastAllPage("success", response.message);
                    $('#address-modal').modal('hide');
                } else if (response.status === "info") {
                    showToastAllPage("info", response.message);
                    $('#address-modal').modal('hide');
                } else {
                    showToastAllPage("warning", response.message);
                    $('#address-modal').modal('hide');
                }
            }
        })
    });
})();