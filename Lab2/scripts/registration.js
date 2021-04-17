let signInButton = document.getElementById("registration-button");

signInButton.addEventListener("click", signUpValidation);

function signUpValidation() {
    let isThereValidationProblems = false;
    let mail = document.getElementById("registration-email");
    if(!validateEmail(mail.value)) {
        mail.addEventListener("input",  setNormalBorderColor, false);
        isThereValidationProblems = true;
        mail.classList.remove("errorAnimation");
        void mail.offsetWidth;
        mail.classList.add("errorAnimation");
    }

    let name = document.getElementById("registration-name");
    if(name.value.length < 4 || name.value.length > 20) {
        name.addEventListener("input",  setNormalBorderColor, false);
        isThereValidationProblems = true;
        name.classList.remove("errorAnimation");
        void name.offsetWidth;
        name.classList.add("errorAnimation");
    }

    let firstPassword = document.getElementById("registration-first-password");
    let secondPassword = document.getElementById("registration-second-password");
    if(firstPassword.value.length < 4 || firstPassword.value.length > 20 || firstPassword.value != secondPassword.value) {
        firstPassword.addEventListener("input",  setNormalBorderColor, false);
        isThereValidationProblems = true;
        firstPassword.classList.remove("errorAnimation");
        void firstPassword.offsetWidth;
        firstPassword.classList.add("errorAnimation");
        secondPassword.addEventListener("input",  setNormalBorderColor, false);
        secondPassword.classList.remove("errorAnimation");
        void secondPassword.offsetWidth;
        secondPassword.classList.add("errorAnimation");
    }

    if(isThereValidationProblems && document.getElementById("errorMessage") === null) {
        let errorDiv = document.createElement("div");
        errorDiv.id = "errorMessage";
        errorDiv.classList.toggle("errorMessage");
        errorDiv.classList.toggle("errorAnimation");
        let alarm = document.createElementNS("http://www.w3.org/2000/svg", "svg");
        alarm.setAttribute("class", "bi bi-exclamation-triangle-fill alarmIcon");
        let path = document.createElementNS("http://www.w3.org/2000/svg", "path");
        path.setAttribute("d", "M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z");
        alarm.appendChild(path);
        let text = document.createTextNode("Incorrect input, please try again");
        errorDiv.appendChild(alarm);
        errorDiv.appendChild(text);
        document.getElementById("header-registration-container").appendChild(errorDiv);
        let message = document.getElementById("errorMessage");
        message.classList.remove("errorAnimation");
        void message.offsetWidth;
        message.classList.add("errorAnimation");
    }

}

function setNormalBorderColor(e) {
    e.currentTarget.style.borderColor = "#e9f2ff";
}

function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

