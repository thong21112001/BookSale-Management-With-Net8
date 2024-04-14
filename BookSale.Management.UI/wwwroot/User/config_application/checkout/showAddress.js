(function () {
    //Khi click vào add thì toàn bộ đưa về mặc định
    $(document).on('click', '#btn-add', function () {
        $('#address-modal').modal('show');
    });

    //Paypal
    paypal.Buttons({
        createOrder: function (data,action) {
            return action.order.create({
                "purchase_units": [
                    {
                        "amount": {
                            "currency_code": "USD",
                            "value": "1"
                        },
                        "items": []
                    }
                ]
            })
        },

        onApprove: function (data, action) {
            return action.order.capture().then(function (response) {
                console.log(response);

                if (response.status === "COMPLETED") {
                    $("#paypal-order-id").val(response.id);
                }

            })   
        },

        style: {
            layout: 'vertical',
            color: 'blue',
            shape: 'rect',
            label: 'paypal'
        }
    }).render('#paypal-button-container');
})();


$(document).ready(function () {
    $('.address-link').hover(
        function () {
            var name = $(this).data('name');
            var address = $(this).data('address');
            var phone = $(this).data('phone');
            var email = $(this).data('email');
            var info = "Name: " + name + "<br>Address: " + address + "<br>Phone: " + phone + "<br>Email: " + email;
            $('#address-info').html(info);
        },
        function () {
            $('#address-info').empty();
        }
    );

    $('.address-link').click(function (e) {
        e.preventDefault(); // Ngăn chặn chuyển hướng khi nhấp vào thẻ a
        var name = $(this).data('name');
        var address = $(this).data('address');
        var phone = $(this).data('phone');
        var email = $(this).data('email');

        // Gán dữ liệu vào các input tương ứng
        $('input[name="Fullname"]').val(name);
        $('input[name="PhoneNumber"]').val(phone);
        $('input[name="Email"]').val(email);
        $('input[name="Address"]').val(address);
    });
});