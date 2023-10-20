﻿// Email validation
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

function checkPassword() {
    var password = document.getElementById("password").value;
    var error = document.getElementById("errorPassword");
    var patternLength = /^.{8,16}$/; 
    var patternSpecialChar = /[!@#$%^&*]/; 
    var patternNumber = /[0-9]/; 
    var patternUpperCase = /[A-Z]/; 
    var patternLowerCase = /[a-z]/; 

    if (password.trim() === "") { 
        error.textContent = "Password can't be empty";
        return false;
    } else if (!patternLength.test(password)) {
        error.textContent = "Password should be between 8 and 16 characters";
        return false;
    } else if (!patternSpecialChar.test(password)) {
        error.textContent = "Password should contain at least one special character";
        return false;
    } else if (!patternNumber.test(password)) {
        error.textContent = "Password should contain at least one number";
        return false;
    } else if (!patternUpperCase.test(password)) {
        error.textContent = "Password should contain at least one uppercase character";
        return false;
    } else if (!patternLowerCase.test(password)) {
        error.textContent = "Password should contain at least one lowercase character";
        return false;
    } else {
        error.textContent = "";
    }
    return true;
}



// Form validation
function validate() {
    var checkValid = true;

    checkValid = checkEmail() && checkValid;
    checkValid = checkPassword() && checkValid;

    return checkValid;

}