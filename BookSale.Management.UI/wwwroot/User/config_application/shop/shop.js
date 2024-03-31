﻿(function () {

    function initial() {
        registerEvents();
    };
    
    function registerEvents() {

        $(document).on('click', '#btn-load-more', function () {

            const genreId = $('#current-genre').val();
            const pageIndex = parseInt($('#current-page-index').val()) + 1;
            const rootImagePath = $('#baseImageUrl').val();

            $.ajax({
                url: `/shop/getbookbypagination?genre=${genreId}&pageIndex=${pageIndex}`,
                method: 'GET',
                success: function (response) {

                    if (response) {

                        let html = '';

                        response.books.forEach((book, index) => {
                            html += `<div class="col-md-6 col-lg-6 col-xl-4">
                                            <div class="rounded position-relative fruite-item">

                                                <div class="fruite-img">
                                                    <img src="${buildImageUrl(book.image, rootImagePath)}" class="img-fluid w-100 rounded-top" alt="">
                                                </div>
                                                <div class="p-4 border border-secondary border-top-0 rounded-bottom">
                                                    <h5 style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">${book.title}</h5>
                                                    <p>Code: ${book.code}</p>
                                                    <p style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">Tác giả: ${book.author}</p>
                                                    <p class="text-dark fs-5 fw-bold mb-0">Giá: ${book.price.toLocaleString('vi-VN', {
                                style: 'currency',
                                currency: 'VND'
                            })}</p>
                                                    <a class="btn btn-add-cart border border-secondary rounded-pill px-3 text-primary" data-code="${book.code}"><i class="fa fa-shopping-bag me-2 text-primary"></i> Add to cart</a>
                                                </div>
                                            </div>
                                        </div>`;
                        });

                        $('#content-book').append(html);

                        $('#txt-pagination').html(`${response.currentRecords} items of ${response.totalRecords}`);

                        if (response.isDisableButton) {
                            $('#btn-load-more').attr('disabled', 'disabled');
                        }

                        $('#progress-bar').attr('style', `width: ${response.progessingValue}%`)

                        $('#current-page-index').val(pageIndex);
                    }

                }
            });

        });

    };

    function buildImageUrl(imageName, rootImagePath) {
        if (!imageName) {
            return rootImagePath + 'image.png'; // Mặc định
        } else if (imageName.startsWith("https:")) {
            return imageName;
        } else {
            return rootImagePath + imageName;
        }
    }

    initial();

})();