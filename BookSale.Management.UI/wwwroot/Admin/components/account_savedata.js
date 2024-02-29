(function () {

    const img = document.getElementById('img-avatar');

    document.getElementById('Avatar').onchange = function () {
        const input = document.getElementById('Avatar').files[0];

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