window.onload = addAlerts;

function addAlerts() {
    let buttons = document.querySelectorAll(".premium-panel");

    for (let i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener("click", showAlert);
    }
}

function showAlert() {
    window.alert("Ta funkcja zostanie zaimplementowana w pełnej, komercyjnej wersji oprogramowania HelloWORD");
}