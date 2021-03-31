window.onload = addAlerts;

function addAlerts() {
    let buttons = document.querySelectorAll(".premium-panel");

    for (let i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener("click", showAlert);
    }

    document.getElementById("buyAlertButton").addEventListener("click", hideAlert);
}

function showAlert() {
    $("#buyAlert").show();
}

function hideAlert() {
    $("#buyAlert").hide();
}