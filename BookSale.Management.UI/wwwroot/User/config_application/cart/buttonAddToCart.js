$(document).on('click', '.btn-add-cart', function () {

    const code = $(this).data('code'); //Trong thẻ a có class btn-add-cart và phần data-code để lấy code sách

    $.ajax({
        url: '/cart/add',
        method: 'POST',
        data: { codeBook : code, quantity : 1 },    //Từ CartModel
        success: function (count) {
            if (count === -1) {
                //Hieern thi thong bao loi
            } else {
                $('.cart-number').text(count);
                showToastAllPage("success","Thêm vào giỏ thành công !!!");
            }
        }
    });

});