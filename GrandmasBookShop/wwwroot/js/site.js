document.addEventListener("DOMContentLoaded", function () {
    // Create the "Back to Top" button
    const backToTopBtn = document.createElement("button");
    backToTopBtn.id = "back-to-top";
    backToTopBtn.textContent = "↑ Top";

    // Style the button using inline styles
    Object.assign(backToTopBtn.style, {
        position: "fixed",
        bottom: "20px",
        right: "20px",
        padding: "10px 15px",
        fontSize: "16px",
        border: "none",
        borderRadius: "5px",
        backgroundColor: "#6d4c41",
        color: "#fff",
        cursor: "pointer",
        display: "none",
        zIndex: "1000"
    });

    document.body.appendChild(backToTopBtn);

    // Show or hide the button based on scroll position
    window.addEventListener("scroll", function () {
        if (window.scrollY > 300) {
            backToTopBtn.style.display = "block";
        } else {
            backToTopBtn.style.display = "none";
        }
    });

    // Smooth scroll to the top when the button is clicked
    backToTopBtn.addEventListener("click", function () {
        window.scrollTo({
            top: 0,
            behavior: "smooth"
        });
    });
});
