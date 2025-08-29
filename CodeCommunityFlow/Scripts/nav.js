document.addEventListener("DOMContentLoaded", () => {
    const currentPath = window.location.pathname.toLowerCase();

    if (currentPath.includes("/account/register")) {
        document.querySelector("a[href='/account/register']")?.setAttribute("id", "active");
    } else if (currentPath.includes("/account/login")) {
        document.querySelector("a[href='/account/login']")?.setAttribute("id", "active");
    }
});
