(function () {
    $(document).on('click', '#btn-generate', function () {
        $.ajax({
            url: '/admin/book/generatecodebook',
            success: function (response) {
                $('#Code').val(response);
            }
        });
    });
})();