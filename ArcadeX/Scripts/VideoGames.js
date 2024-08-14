document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.querySelector("[data-search]");
    const cardsList = document.querySelector("[data-game-cards-container]");

    searchInput.addEventListener("input", (e) => {
        const value = e.target.value.toLowerCase();
        const cards = cardsList.querySelectorAll(".card");

        cards.forEach(card => {
            const title = card.querySelector("[data-title]").textContent.toLowerCase();
            const genre = card.querySelector("[data-genre]").textContent.toLowerCase();
            const platform = card.querySelector("[data-platform]").textContent.toLowerCase();
            const publisher = card.querySelector("[data-publisher]").textContent.toLowerCase();
            const release = card.querySelector("[data-release]").textContent.toLowerCase();

            const isVisible = title.includes(value) ||
                              genre.includes(value) ||
                              platform.includes(value) ||
                              publisher.includes(value) ||
                              release.includes(value);

            card.style.display = isVisible ? "block" : "none";
        });
    });
});
