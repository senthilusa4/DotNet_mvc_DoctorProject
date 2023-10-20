// Patient name validatation
function checkPatientName() {
    var name = document.getElementById("patientName").value;
    var error = document.getElementById("errorPatientName");
    var pattern = (/^[a-zA-Z]*$/);

    if (name == "") {
        error.textContent = "First name can't be empty";
        return false;
    }
    else if (!pattern.test(name)) {
        error.textContent = "Name contain only alphabets";
        return false;
    }
    else {
        error.textContent = "";
    }
    return true;
}


// issue validatation
function checkIssue() {
    var name = document.getElementById("issue").value;
    var error = document.getElementById("errorIssue");
    var pattern = (/^[a-zA-Z]*$/);

    if (name == "") {
        error.textContent = "Issue can't be empty";
        return false;
    }
    else if (!pattern.test(name)) {
        error.textContent = "Issue contain only alphabets";
        return false;
    }
    else {
        error.textContent = "";
    }
    return true;
}

// Date validation
function checkDate() {
    var date = document.getElementById("date").value;
    var errorDate = document.getElementById("errorDate");

    if (date === "") {
        errorDate.textContent = "Visit Date must be filled";
        return false;
    } else {
        errorDate.textContent = "";
    }
    return true;
}


// Time validation
function checkTime() {
    var time = document.getElementById("time").value;
    var errorTime = document.getElementById("errorTime");

    if (time === "") {
        errorTime.textContent = "Visit Time must be filled";
        return false;
    } else {
        errorTime.textContent = "";
    }
    return true;
}


// Form validation
function validate() {
    var checkValid = true;

    checkValid = checkPatientName() && checkValid;
    checkValid = checkIssue() && checkValid;
    checkValid = checkDate() && checkValid;
    checkValid = checkTime() && checkValid;

    return checkValid;
}
