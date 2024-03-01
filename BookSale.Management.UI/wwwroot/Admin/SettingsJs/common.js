/**
 * show Toast [Success,Information,Error,Warning]
 * @param {any} type
 * @param {any} text
 * @param {any} timeOut
 */

function showToast(type, text, timeOut = 3000) {
    $(document).toast({
        heading: type,
        text: text,
        position: 'top-right',
        icon: type === 'Information' ? 'info' : type.toLowerCase(),
        hideAfter: timeOut
    })
}