(function () {
    function intital() {
        registerDateTime();
        registerEvent();
    }


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
        $(document).on('click', '#btn-submit', function () {

            const fromD = $('#start').val();
            const toD = $('#end').val();
            const genre = $('#option-genre').val();
            const status = $('#option-status').val();

            if (!fromD || !toD) {
                showToastAllPage("error", "Hãy chọn thời gian trước khi lọc !!!");
                return;
            }

            location.href = `/admin/report?fromday=${fromD}&today=${toD}&genreid=${genre}&status=${status}`;
        });
    }

    intital();
})();