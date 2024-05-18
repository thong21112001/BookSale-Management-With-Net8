$(document).on('click', '.set-btn-add-quantity-cart', function () {

    const code = $(this).data('code'); //Trong thẻ a có class set-btn-add-cart và phần data-code để lấy code sách
    const quantityInput = parseInt($('#product-quantity').val(), 10); // Lấy số lượng từ input và chuyển thành số nguyên

    if (quantityInput < 1 || isNaN(quantityInput)) {
        showToastAllPage("error", "Số lượng sản phẩm phải lớn hơn hoặc bằng 1.");
        return; // Ngăn chặn việc gửi yêu cầu AJAX nếu số lượng nhỏ hơn 1 hoặc không phải là số hợp lệ
    }

    $.ajax({
        url: '/cart/add',
        method: 'POST',
        data: { codeBook: code, quantity: quantityInput },    //Từ CartModel
        success: function (count) {
            if (count === -1) {
                showToastAllPage("error", "Thêm vào giỏ thất bại...");
            } else {
                updateCartCount();
                showToastAllPage("success", "Thêm vào giỏ thành công !!!");
            }
        }
    });

});