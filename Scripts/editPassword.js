window.onload = addPasswordResetFunctionality;

function addPasswordResetFunctionality() {
    let buttonCode = document.getElementById('buttonCode');
    buttonCode.addEventListener('click', () => { showAlert(buttonCode) });
    buttonCode.addEventListener('click', () => { sendCode()})
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

function sendCode() {
    $.ajax({
        url: "SendPasswordResetCode",
        cache: false,
        dataType: "JSON",
        data: { userEmail: document.getElementById('userData').value },
        success: function() {
            alert("wysłano email");
        },
        failure: function () {
            alert("maila nie wysłano");
        }
    });
}
