$(document).ready(function () {
    $('.btnDeleteItem').click(function () {
        var codeBook = $(this).data('product-code');

        $.ajax({
            url: '/cart/delete',
            type: 'POST',
            data: { codeBook: codeBook },
            success: function (result) {
                if (result.success) {
                    // Xóa sản phẩm khỏi giao diện
                    $(`tr[data-code="${result.codeBook}"]`).remove();
                    // Cập nhật tổng giá tiền của giỏ hàng
                    $('#cartTotal').text(result.cartTotal);
                    // Cập nhật tổng item của giỏ hàng
                    $('#itemCartTotal').text(result.itemCartTotal);
                    //Cập nhập lại số lượng hiển thị item trên giỏ hàng
                    updateCartCount();
                    //Hiển thị thông báo thành công
                    showToastAllPage("success", "Xoá sản phẩm thành công !!!");
                } else {
                    showToastAllPage("error", "Xoá sản phẩm thất bại...");
                }
            }
        });
    });
});