(function () {

    const img = document.getElementById('img-avatar');

    document.getElementById('Image').onchange = function () {
        const input = document.getElementById('Image').files[0];

        if (input) {
            img.src = URL.createObjectURL(input);
        }
    }

    //Xử lý khi ảnh đã bị xoá và thay bằng ảnh khác
    img.onerror = function () {
        onErrorImages();
    }

    function onErrorImages() {
        img.src = "/images/image.png";
        img.alt = "no image";
    }
})();

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