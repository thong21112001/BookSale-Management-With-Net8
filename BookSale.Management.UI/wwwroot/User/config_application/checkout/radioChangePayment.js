// Lấy các phần tử input radio và các phần DIV tương ứng
const cashRadio = document.querySelector('input[value="1"]');
const paypalRadio = document.querySelector('input[value="2"]');
const cashDiv = document.querySelector('#cashDiv');
const paypalDiv = document.querySelector('#paypalDiv');

// Ẩn phần DIV Paypal ban đầu
paypalDiv.style.display = 'none';

// Đặt sự kiện "change" cho input radio
cashRadio.addEventListener('change', function () {
    if (cashRadio.checked) {
        cashDiv.style.display = 'block'; // Hiển thị phần DIV Cash
        paypalDiv.style.display = 'none'; // Ẩn phần DIV Paypal
    }
});

paypalRadio.addEventListener('change', function () {
    if (paypalRadio.checked) {
        paypalDiv.style.display = 'block'; // Hiển thị phần DIV Paypal
        cashDiv.style.display = 'none'; // Ẩn phần DIV Cash
    }
});