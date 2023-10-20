// First name validation
function checkFname() {
    var name = document.getElementById("Fname").value;
    var error = document.getElementById("errorFname");
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


// Phone Number validation
function checkNumber() {
    var number = document.getElementById("number").value;
    var error = document.getElementById("errorNumber");
    var pattern = /[0-9]/;

    if (number == "") {
        error.textContent = "Number can't be empty";
        return false;
    }
    else if (!pattern.test(number)) {
        error.textContent = "Must contain number's"
        return false;
    }
    else if (number.length != 10) {
        error.textContent = "Number contain 10 digit";
        return false;
    }
    else {
        error.textContent = "";
    }
    return true;
}


function checkFeedback() {
    var address = document.getElementById("Feedback").value;
    var error = document.getElementById("errorFeedback");
    var patternSpecialChar = /[!@#$%^&*]/;

    if (address.trim() === "") {
        error.textContent = "comments can't be empty";
        return false;
    } else if (patternSpecialChar.test(address)) {
        error.textContent = "Special characters are not allowed in the comments";
        return false;
    } else if (address.length < 20) {
        error.textContent = "Comments should be at least 20 characters long";
        return false;
    } else {
        error.textContent = "";
    }
    return true;
}



// Email validation
function checkEmail() {
    var email = document.getElementById("email").value;
    var error = document.getElementById("errorEmail");
    var pattern = /[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    if (email == "") {
        error.textContent = "Email can't be empty";
        return false;
    }
    else if (!pattern.test(email)) {
        error.textContent = "Enter valid e-mail address";
        return false;
    }
    else {
        error.textContent = "";
    }
    return true;
}



// Form validation
function validate() {
    var checkValid = true;

    checkValid = checkFname() && checkValid;
    checkValid = checkNumber() && checkValid;
    checkValid = checkEmail() && checkValid;
    checkValid = checkFeedback() && checkValid;

    return checkValid;
}
