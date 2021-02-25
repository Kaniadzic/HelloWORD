window.onload = checkCookiesAgreement;

function checkCookiesAgreement() {
    cookiesAgreed = localStorage.getItem('HelloWORDCookiesAgreed');

    if (!cookiesAgreed) {
        cookiesAlert();
    }
}

function cookiesAlert() {
    let acceptCookiesButton = document.getElementById('acceptCookies');
    let cookiesAlert = document.getElementById('cookiesAlert');
    cookiesAlert.style.display = "flex";

    acceptCookiesButton.addEventListener('click', agreeToCookies);
}

function agreeToCookies() {
    localStorage.setItem('HelloWORDCookiesAgreed', 'True');
    let cookiesAlert = document.getElementById('cookiesAlert');
    cookiesAlert.style.display = "none";
}