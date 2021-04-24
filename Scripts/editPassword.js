window.onload = addPasswordResetFunctionality;

function addPasswordResetFunctionality() {
    let buttonCode = document.getElementById('buttonCode');
    buttonCode.addEventListener('click', () => { showAlert(buttonCode) });
    buttonCode.addEventListener('click', () => { sendCode()})
}

function showAlert(buttonCode) {
    if (document.getElementById('userData').value == "") {
        event.preventDefault();
        event.stopImmediatePropagation();
        return false;
    }

    let buttonPost = document.getElementById('buttonPost');
    buttonCode.classList.add('d-none');
    buttonPost.classList.remove('d-none');

    let codeAlert = $('#codeAlert');
    codeAlert.show();

    let codeAlertButton = document.getElementById('codeAlertButton');
    codeAlertButton.addEventListener('click', () => { codeAlert.hide() });
    codeAlertButton.addEventListener('click', () => { buttonPost.disabled = false });

    let inputs = document.querySelectorAll('.form-control');
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }
}

function sendCode() {
    if (document.getElementById('userData').value == "") {
        event.preventDefault();
        event.stopImmediatePropagation();
        return false;
    }

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
