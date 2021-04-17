window.onload = dupa;

function dupa() {
    let buttonCode = document.getElementById('buttonCode');
    buttonCode.addEventListener('click', () => { showAlert(buttonCode) });
}

function showAlert(buttonCode) {
    let buttonPost = document.getElementById('buttonPost');
    buttonCode.classList.add('d-none');
    buttonPost.classList.remove('d-none');

    let codeAlert = $('#codeAlert');
    codeAlert.show();

    let codeAlertButton = document.getElementById('codeAlertButton');
    codeAlertButton.addEventListener('click', () => { codeAlert.hide() });
    codeAlertButton.addEventListener('click', () => { buttonPost.disabled = false });
}
