
document.addEventListener("DOMContentLoaded", () => {
    const ContactForm = document.getElementById("Contact-Form");
    if (!ContactForm) return;

    const emailInput = document.getElementById("txtWorkEmail");
    const emailFeedback = document.getElementById("email-feedback");
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    let emailIsValid = false;

    function validateEmail() {
        const email = emailInput.value.trim();
    emailInput.classList.remove("is-valid", "is-invalid");
    emailFeedback.textContent = "";
    emailIsValid = false;

    if (!email) {
        emailInput.classList.add("is-invalid");
    emailFeedback.textContent = "Email is required.";
    return;
        }

    if (!emailRegex.test(email)) {
        emailInput.classList.add("is-invalid");
    emailFeedback.textContent = "Invalid email format.";
    return;
        }

    // Fetch check
    fetch("/api/account/checkWorkemail?workEmail=" + encodeURIComponent(email))
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
    emailFeedback.textContent = "Error checking email.";
    emailIsValid = false;
            });
    }

    // Validate only when focus leaves the email field
    emailInput.addEventListener("blur", validateEmail);

    ContactForm.addEventListener("submit", (e) => {
        const email = emailInput.value.trim();
        if (!email || !emailRegex.test(email) || !emailIsValid) {
            emailInput.classList.add("is-invalid");
            emailFeedback.textContent = "Email is not valid or already in use.";
            e.preventDefault();
            e.stopPropagation();
        } else {
            emailInput.classList.add("is-valid");
        }
    });
});
