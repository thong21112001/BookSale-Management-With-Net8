const inputFields = document.querySelectorAll('#input-fields input.required-field');
const paymentSections = document.querySelectorAll('.hidden');
const proceedButton = document.querySelector('#showPayment');

function togglePaymentSection() {
    const allFieldsFilled = Array.from(inputFields).every(input => input.value.trim() !== '' && input.checkValidity());

    paymentSections.forEach(section => {
        if (allFieldsFilled) {
            section.classList.remove('hidden');
        } else {
            section.classList.add('hidden');
        }
    });
}

proceedButton.addEventListener('click', togglePaymentSection);