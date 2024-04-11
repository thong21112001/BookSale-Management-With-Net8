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