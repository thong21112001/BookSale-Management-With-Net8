function mapObjectToModalView(modalView) {
    if (typeof modalView !== 'object') {
        return;
    }

    for (const item in modalView) {
        if (modalView.hasOwnProperty(item)) {
            const [firstCharacter, ...restChar] = item // trả về object toàn chữ thường -> name -> Name
            const capitalText = `${firstCharacter.toLocaleUpperCase()}${restChar.join('')}`;
            //Kiểm tra xem phần tử có phải là checkbox không
            const isCheckbox = $(`#${capitalText}`).is(':checkbox');
            if (isCheckbox) {
                // Sử dụng prop() cho checkbox
                $(`#${capitalText}`).prop('checked', modalView[item]);
            }
            else {
                // Sử dụng val() cho các phần tử khác
                $(`#${capitalText}`).val(modalView[item]);
            }
        }
    }
}