//Hard code simulates request to the server
accounts = [
    {
        login: "a",
        password: "a"
    },
    {
        login: "user@gmail.com",
        password: "user"
    }
]

function isUserValid(login, password) {
    return accounts.some(e => e.login === login && e.password === password);
}
//--------------------

let signInButton = document.getElementById("sing-in-button");

signInButton.addEventListener("click", signInValidation);

function signInValidation() {
    let login = document.getElementById("sing-in-login").value;
    let password = document.getElementById("sing-in-password").value;
    if(isUserValid(login, password)) {
        document.location.href = "../html/index.html";
    } else if (document.getElementById("errorMessage") === null) {
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
        document.getElementById("header-sign-in-container").appendChild(errorDiv);
        let forms = document.getElementsByTagName("input");
        Array.prototype.forEach.call(forms, function(form) {
            form.addEventListener("input",  setNormalBorderColor, false);
            void form.offsetWidth;
            form.classList.add("errorAnimation");
        }); 
    } else {
        let forms = document.getElementsByTagName("input");
        Array.prototype.forEach.call(forms, function(form) {
            form.classList.remove("errorAnimation");
            void form.offsetWidth;
            form.classList.add("errorAnimation");
        });
        let message = document.getElementById("errorMessage");
        message.classList.remove("errorAnimation");
        void message.offsetWidth;
        message.classList.add("errorAnimation");
    }
}

function setNormalBorderColor(e) {
    e.currentTarget.style.borderColor = "#e9f2ff";
}

