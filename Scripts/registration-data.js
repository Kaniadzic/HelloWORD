window.onload = initialize;

function initialize() {
    addSubmitListener();
    loadRegistrationData();
}

function addSubmitListener() {
    let submitButton = document.getElementById("RegistrationSubmit");
    submitButton.addEventListener("click", saveRegistrationData);
}

// zapisanie danych z formularza do sesji
function saveRegistrationData() {
    let login = document.getElementById("Login").value;
    let password = document.getElementById("Password").value;
    let passwordRepeat = document.getElementById("PasswordRepeat").value;
    let firstName = document.getElementById("FirstName").value;
    let lastName = document.getElementById("LastName").value;
    let email = document.getElementById("Email").value;
    let emailRepeat = document.getElementById("EmailRepeat").value;

    let registrationData = [login, password, passwordRepeat, firstName, lastName, email, emailRepeat];
    sessionStorage.setItem('registrationData', JSON.stringify(registrationData));
}

// załadowanie danych z sesji do pól formularza oraz wyczyszczenie sesji
function loadRegistrationData() {
    let registrationData = JSON.parse(sessionStorage.getItem('registrationData'));

    if (registrationData) {
        console.log(registrationData);

        let login = document.getElementById("Login");
        let password = document.getElementById("Password");
        let passwordRepeat = document.getElementById("PasswordRepeat");
        let firstName = document.getElementById("FirstName");
        let lastName = document.getElementById("LastName");
        let email = document.getElementById("Email");
        let emailRepeat = document.getElementById("EmailRepeat");

        login.value = registrationData[0];
        password.value = registrationData[1];
        passwordRepeat.value = registrationData[2];
        firstName.value = registrationData[3];
        lastName.value = registrationData[4];
        email.value = registrationData[5];
        emailRepeat.value = registrationData[6];

        sessionStorage.removeItem('registrationData');
    }
}