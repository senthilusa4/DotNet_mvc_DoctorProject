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


// Last name validation
function checkLname() {
    var name = document.getElementById("Lname").value;
    var error = document.getElementById("errorLname");
    var pattern = (/^[a-zA-Z]*$/);

    if (name == "") {
        error.textContent = "Last name can't be empty";
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

// DOB validation
function checkDate() {
    var date = document.getElementById("dob").value;
    var errorDate = document.getElementById("errorDob");

    if (date === "") {
        errorDate.textContent = "Date of birth must be filled";
        return false;
    } else {
        errorDate.textContent = "";
    }
    return true;
}


    // Age calculation validation
    function checkAge() {
        var userinput = document.getElementById("dob").value;

        var dob = new Date(userinput);
        var today = new Date();
        var result_age = today.getFullYear() - dob.getFullYear();
        
        var display = document.getElementById('age');
        display.value = result_age;

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

function checkAddress() {
    var address = document.getElementById("address").value;
    var error = document.getElementById("errorAddress");
    var patternSpecialChar = /[!@#$%^&*]/; 

    if (address.trim() === "") {
        error.textContent = "Address can't be empty";
        return false;
    } else if (patternSpecialChar.test(address)) {
        error.textContent = "Special characters are not allowed in the address";
        return false;
    } else if (address.length < 20) {
        error.textContent = "Address should be at least 20 characters long";
        return false;
    } else {
        error.textContent = "";
    }
    return true;
}



// State validation
function checkState() {
    var state = document.getElementById("state").value;
    var error = document.getElementById("errorState");

    if (state == "") {
        error.textContent = "Must select state";
        return false;
    }
    else {
        error.textContent = "";
    }
    return true;
}

function chooseState(state, city) {
    var state1 = document.getElementById(state);
    var city1 = document.getElementById(city);

    city1.innerHTML = "";

    if (state1.value == "tamilnadu") {
        var option = ['chennai|Chennai', 'tirupur|Tirupur', 'erode|Erode'];
    }

    else if (state1.value == "kerla") {
        var option = ['palakad|Palakadu', 'kochin|Kochin', 'kollam|Kollam'];
    }

    else if (state1.value == "karnataka")
        var option = ['bangalore|Bangalore', 'mangalore|Mangalore']

    for (var o in option) {
        var split = option[o].split("|");
        var newoption = document.createElement("option");

        newoption.value = split[0];
        newoption.innerHTML = split[1];
        city1.options.add(newoption);
    }
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

// Password validation
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


// Confirm password validation
function checkConfirmPassword() {
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("confirmPassword").value;
    var error = document.getElementById("errorConfirmPassword");

    if (password != password2) {
        error.textContent = "Password mismatch";
        return false;
    }
    else if (password2 == "") {
        error.textContent = "Password can't be empty";
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
    checkValid = checkLname() && checkValid;
    checkValid = checkDate() && checkValid;
    checkValid = checkNumber() && checkValid;
    checkValid = checkEmail() && checkValid;
    checkValid = checkAddress() && checkValid;
    checkValid = checkState() && checkValid;
    checkValid = checkPassword() && checkValid;
    checkValid = checkConfirmPassword() && checkValid;

    return checkValid;
}
