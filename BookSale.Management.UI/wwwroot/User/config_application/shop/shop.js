(function () {

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
                    if (response && Array.isArray(response.Books) && response.Books.length > 0) {
                        console.log('Books found:', response.Books);

                        let html = '';

                        response.Books.forEach((book, index) => {
                            html += `<div class="col-md-6 col-lg-6 col-xl-4">
                                            <div class="rounded position-relative fruite-item">

                                                <div class="fruite-img">
                                                    <img src="${buildImageUrl(book.Image, rootImagePath)}" class="img-fluid w-100 rounded-top" alt="">
                                                </div>
                                                <div class="p-4 border set-border-primary border-top-0 rounded-bottom">
                                                    <a href="/ProductDetail?code=${book.Code}" class="text-ellipsis set-a">${book.Title}</a>
                                                    <p>Code: ${book.Code}</p>
                                                    <p style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">Tác giả: ${book.Author}</p>
                                                    <p class="text-dark fs-5 fw-bold mb-0">Giá: ${book.Price.toLocaleString('vi-VN', {
                                style: 'currency',
                                currency: 'VND'
                            })}</p>
                                                    <a class="btn set-btn-add-cart border rounded-pill px-3 text-white" data-code="${book.Code}"><i class="fa fa-shopping-bag me-2"></i> Add to cart</a>
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
                    else {
                        // Handle case where no books are returned or books array is empty
                        console.error('No books found in the response');
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