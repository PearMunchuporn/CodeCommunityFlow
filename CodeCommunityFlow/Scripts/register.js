document.addEventListener("DOMContentLoaded", () => {
    const registerForm = document.getElementById("registerForm");
    if (!registerForm) return;

    const inputs = registerForm.querySelectorAll("input");
    const emailInput = document.getElementById("txtEmail");
    const passwordInput = document.getElementById("ppassword");
    const confirmPasswordInput = document.getElementById("pconfirmpassword");
    const confirmFeedback = document.getElementById("confirm-feedback");
    const emailFeedback = document.getElementById("email-feedback");
    let emailIsValid = false;

    function validateField(input) {
        input.classList.remove("is-valid", "is-invalid");

        if (!input.value.trim()) {
            input.classList.add("is-invalid");
            if (input === confirmPasswordInput && confirmFeedback) {
                confirmFeedback.textContent = "Please confirm your password.";
            }
            return false;
        }

        if (input.type === "email") {
            const isEmail = /^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(input.value.trim());
            if (!isEmail) {
                input.classList.add("is-invalid");
                if (emailFeedback) emailFeedback.textContent = "Invalid email format.";
                emailIsValid = false;
                return false;
            }
        }

        if (input === confirmPasswordInput) {
            const confirmValue = confirmPasswordInput.value.trim();
            const passwordValue = passwordInput.value.trim();

            if (!confirmValue) {
                confirmPasswordInput.classList.add("is-invalid");
                confirmFeedback.textContent = "Please confirm your password.";
                return false;
            }

            if (confirmValue !== passwordValue) {
                confirmPasswordInput.classList.add("is-invalid");
                confirmFeedback.textContent = "Passwords do not match.";
                return false;
            }

            confirmPasswordInput.classList.remove("is-invalid");
            confirmPasswordInput.classList.add("is-valid");
            confirmFeedback.textContent = "";
            return true;
        }

        input.classList.add("is-valid");
        return true;
    }


    function validatePasswordMatch() {
        confirmPasswordInput.classList.remove("is-valid", "is-invalid");
        if (passwordInput.value !== confirmPasswordInput.value) {
            confirmPasswordInput.classList.add("is-invalid");
            confirmFeedback.textContent = "Passwords do not match.";
            return false;
        }
        confirmPasswordInput.classList.add("is-valid");
        confirmFeedback.textContent = "";
        return true;
    }

    inputs.forEach(input => {
        input.addEventListener("input", () => {
            validateField(input);
            if (input === passwordInput || input === confirmPasswordInput) {
                validatePasswordMatch();
            }
        });
    });

    emailInput.addEventListener("blur", () => {
        const email = emailInput.value.trim();
        const isEmail = /^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email);

        if (!email || !isEmail) {
            emailIsValid = false;
            return;
        }

        fetch("/api/account/check-email?email=" + encodeURIComponent(email))
            .then(res => res.json())
            .then(data => {
                emailInput.classList.remove("is-valid", "is-invalid");
                if (data.exists) {
                    emailInput.classList.add("is-invalid");
                    emailFeedback.textContent = "Email already exists.";
                    emailIsValid = false;
                } else {
                    emailInput.classList.add("is-valid");
                    emailFeedback.textContent = "";
                    emailIsValid = true;
                }
            })
            .catch(() => {
                emailInput.classList.add("is-invalid");
                emailFeedback.textContent = "Server error while checking email.";
                emailIsValid = false;
            });
    });

    registerForm.addEventListener("submit", (e) => {
        let isFormValid = true;
        inputs.forEach(input => {
            if (!validateField(input)) isFormValid = false;
        });

        if (!validatePasswordMatch()) isFormValid = false;

        if (!emailIsValid) {
            emailInput.classList.add("is-invalid");
            emailFeedback.textContent = "Email is not valid or already in use.";
            isFormValid = false;
        }
   
        const confirmValid = validateField(confirmPasswordInput);
        if (!confirmValid) { 
            confirmFeedback.textContent = "Password comfirmation is required.";
            isFormValid = false;
        }

        if (!isFormValid) {
            e.preventDefault();
            e.stopPropagation();
        }
    });
    confirmPasswordInput.addEventListener("input", function () {
        validateField(confirmPasswordInput);
    });
});
