document.addEventListener("DOMContentLoaded", function () {
    // Thêm sự kiện change cho dropdown
    document.getElementById("imageSource").addEventListener("change", function () {
        var selectedValue = this.value;
        if (selectedValue === "input") {
            document.getElementById("imageInput").style.display = "block";
            document.getElementById("inputImage").style.display = "block";
            document.getElementById("imageString").style.display = "none";
            document.getElementById("ImageText").value = "";
        } else if (selectedValue === "string") {
            document.getElementById("imageInput").style.display = "none";
            document.getElementById("inputImage").style.display = "none";
            document.getElementById("imageString").style.display = "block";
            document.getElementById("Image").value = ""; // Xoá giá trị của input file
            onErrorImages(); // Gọi hàm onErrorImages() khi chuyển sang chế độ chuỗi string
        }
    });

    // Hàm xử lý khi ảnh đã bị xoá và thay bằng ảnh mặc định
    function onErrorImages() {
        const img = document.getElementById('img-avatar');
        img.src = "/images/image.png";
        img.alt = "no image";
    }
});