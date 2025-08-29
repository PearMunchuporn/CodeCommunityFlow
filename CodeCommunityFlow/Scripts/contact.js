
document.addEventListener("DOMContentLoaded", () => {
    const contactForm = document.getElementById("Contact-Form");
    if (!contactForm) return;

    const emailInput = document.getElementById("txtWorkEmail");
    const emailFeedback = document.getElementById("email-feedback");
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    let emailChecked = false;

    function validateEmailFormat(input) {
        const value = input.value.trim();
    input.classList.remove("is-valid", "is-invalid");
    emailFeedback.textContent = "";
    emailChecked = false;

    if (!value) {
        input.classList.add("is-invalid");
    emailFeedback.textContent = "Email is required.";
    return false;
        }

    if (!emailRegex.test(value)) {
        input.classList.add("is-invalid");
    emailFeedback.textContent = "Invalid email format.";
    return false;
        }

    return true;
    }

    function checkEmailFromServer(email) {
        return fetch("/api/account/checkWorkemail?workEmail=" + encodeURIComponent(email))
            .then(res => res.json())
            .then(data => {
        emailInput.classList.remove("is-valid", "is-invalid");

    if (data.exists) {
        emailInput.classList.add("is-invalid");
    emailFeedback.textContent = "Email already exists.";
    return false;
                } else {
        emailInput.classList.add("is-valid");
    emailFeedback.textContent = "";
    return true;
                }
            })
            .catch(() => {
        emailInput.classList.add("is-invalid");
    emailFeedback.textContent = "Error checking email.";
    return false;
            });
    }

    // Lightweight validation on input (format only)
    emailInput.addEventListener("input", () => {
        validateEmailFormat(emailInput);
    });

    // Full validation (format + server) on submit
    contactForm.addEventListener("submit", async (e) => {
        e.preventDefault();
    e.stopPropagation();

    const isValidFormat = validateEmailFormat(emailInput);
    if (!isValidFormat) return;

    const isAvailable = await checkEmailFromServer(emailInput.value.trim());
    if (!isAvailable) return;

    // Everything valid: submit the form
    contactForm.submit();
    });
});

