(function () {
    $(document).on('click', '#btn-generate', function () {

        $.blockUI();

        $.ajax({
            url: '/admin/book/generatecodebook',
            success: function (response) {
                $('#Code').val(response);

                //Dừng 3s
                setTimeout(() => {
                    $.unblockUI();
                }, 3000);
            },
            error: function (response) {
                //Dừng 3s
                setTimeout(() => {
                    $.unblockUI();
                }, 2000);
            }
        });
    });
})();