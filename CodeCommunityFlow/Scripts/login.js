document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.getElementById("loginForm");
    if (!loginForm) return;

    const emailInput = document.getElementById("EmailLogin");
    const passwordInput = document.getElementById("PasswordLogin");
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    function validateField(input) {
        const value = input.value.trim();
        input.classList.remove("is-valid", "is-invalid");

        if (!value) {
            input.classList.add("is-invalid");
            return false;
        }

        if (input.type === "email" && !emailRegex.test(value)) {
            input.classList.add("is-invalid");
            return false;
        }

        input.classList.add("is-valid");
        return true;
    }

    emailInput.addEventListener("input", () => validateField(emailInput));
    passwordInput.addEventListener("input", () => validateField(passwordInput));

    loginForm.addEventListener("submit", (e) => {
        const emailValid = validateField(emailInput);
        const passwordValid = validateField(passwordInput);

        if (!emailValid || !passwordValid) {
            e.preventDefault();
            e.stopPropagation();
        }
    });


});
