(function () {
    function intital() {
        registerDateTime();
        registerEvent();
    }

    //Setting phần datetime lên input
    function registerDateTime() {
        var dateRange = $('#date-range');
        var dateStart = dateRange.find('input[name="start"]').datepicker({
            todayHighlight: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        var dateEnd = dateRange.find('input[name="end"]').datepicker({
            todayHighlight: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        dateStart.on('changeDate', function (e) {
            var startDate = e.date;
            var endDate = dateEnd.datepicker('getDate');

            if (startDate && endDate && startDate > endDate) {
                dateEnd.datepicker('setDate', null);
            }
        });

        dateEnd.on('changeDate', function (e) {
            var endDate = e.date;
            var startDate = dateStart.datepicker('getDate');

            if (endDate && startDate && endDate < startDate) {
                dateStart.datepicker('setDate', null);
            }
        });
    }

    
    function registerEvent() {
        //Nút lọc dữ liệu
        $(document).on('click', '#btn-submit', function () {
            const { fromD, toD, genre, status } = getFilterData();
            location.href = `/admin/report?fromday=${fromD}&today=${toD}&genreid=${genre}&status=${status}`;
        });

        //Nút xuất dữ liệu ra excel
        $(document).on('click', '#btn-export', function () {
            const { fromD, toD, genre, status } = getFilterData();
            location.href = `/admin/report/exportexcelorder?fromday=${fromD}&today=${toD}&genreid=${genre}&status=${status}`;
        });
    }

    //Hàm kiểm tra lọc, lấy dữ liệu đầu vào
    function getFilterData() {
        const fromD = $('#start').val();
        const toD = $('#end').val();

        if (!fromD || !toD) {
            showToastAllPage("error", "Hãy chọn thời gian trước khi lọc !!!");
            return;
        }

        const genre = $('#option-genre').val();
        const status = $('#option-status').val();

        return {fromD, toD, genre, status};
    }

    intital();
})();