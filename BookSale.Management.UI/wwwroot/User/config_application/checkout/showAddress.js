(function () {
    //Khi click vào add thì toàn bộ đưa về mặc định
    $(document).on('click', '#btn-add', function () {
        $('#address-modal').modal('show');
    });

    //Paypal
    paypal.Buttons({
        createOrder: (data, actions) => {
            return fetch("/Checkout/create-paypal-order", {
                method: "post",
                headers: {
                    "Content-Type": "application/json"
                }
            }).then((response) => {
                if (!response.ok) {
                    return response.json().then((err) => {
                        throw error;
                    });
                }
                return response.json();
            }).then((order) => order.id)
            .catch(err => {
                showToastAllPage("warning", "Lỗi khi tiến hành thanh toán !!!");
            });
        },

        onApprove: (data, actions) => {
            const formElement = document.querySelector('form');
            const formData = new FormData(formElement);

            return fetch(`/Checkout/capture-paypal-order?orderId=${data.orderID}`, {
                method: "post",
                body: formData
            }).then((response) => {
                if (!response.ok) {
                    return response.json().then((err) => {
                        throw error;
                    });
                }
                //window.location.href = "/Checkout/PaymentSuccess";
            }).catch(err => {
                showToastAllPage("warning", "Lỗi khi tiến hành thanh toán !!!");
            });
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