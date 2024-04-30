$(document).on('click', '.set-btn-add-cart', function () {

    const code = $(this).data('code'); //Trong thẻ a có class set-btn-add-cart và phần data-code để lấy code sách

    $.ajax({
        url: '/cart/add',
        method: 'POST',
        data: { codeBook : code, quantity : 1 },    //Từ CartModel
        success: function (count) {
            if (count === -1) {
                showToastAllPage("error", "Thêm vào giỏ thất bại...");
            } else {
                updateCartCount();
                showToastAllPage("success","Thêm vào giỏ thành công !!!");
            }
        }
    });

});

function updateCartCount() {
    $.ajax({
        url: '/cart/getcartcount',
        method: 'GET',
        success: function (count) {
            $('.cart-number').text(count);
        }
    });
}