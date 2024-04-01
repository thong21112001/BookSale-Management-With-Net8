function updateQuantity(codeBook, operation) {
    //Goi ajax de cap nhap so luong san pham
    $.ajax({
        url: '/cart/updatequantity',
        method: 'POST',
        data: { codeBook: codeBook, operation: operation },
        success: function (result) {
            // Cập nhật giao diện với kết quả trả về
            if (result.success) {
                if (result.removeItem) {
                    // Xóa sản phẩm khỏi giao diện
                    $(`tr[data-code="${result.codeBook}"]`).remove();

                    // Cập nhật tổng giá tiền của giỏ hàng
                    $('#cartTotal').text(result.cartTotal);

                    // Kiểm tra nếu không còn sản phẩm nào trong giỏ hàng
                    if (result.itemCartTotal === 0) {
                        showToastAllPage("warning", "Giỏ hàng đang trống. Trang sẽ tự động reload sau 8 giây.");

                        // Đặt timeout để reload trang sau 8 giây
                        setTimeout(function () {
                            window.location.reload();
                        }, 8000);
                    }

                    // Cập nhật tổng item của giỏ hàng
                    $('#itemCartTotal').text(result.itemCartTotal);

                    //Cập nhập lại số lượng hiển thị item trên giỏ hàng
                    updateCartCount();

                    showToastAllPage("info", "Sản phẩm đã bị xoá khỏi giỏ hàng.");
                }
                else {
                    // Cập nhật số lượng sản phẩm
                    $(`input[data-code="${codeBook}"]`).val(result.quantity);
                    // Cập nhật tổng giá tiền của sản phẩm, chọn phần tử <p> hiển thị giá tiền của sản phẩm có data-code="${codeBook}".
                    $(`p[data-code="${codeBook}"]`).text(result.itemTotal);
                    // Cập nhật tổng giá tiền của giỏ hàng
                    $('#cartTotal').text(result.cartTotal);
                    // Cập nhật tổng item của giỏ hàng
                    $('#itemCartTotal').text(result.itemCartTotal);
                }
            }
        }
    });
}