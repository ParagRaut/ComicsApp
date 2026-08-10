// Toggles between light and dark themes and remembers the choice.
window.toggleTheme = function () {
    const root = document.documentElement;
    const next = root.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
    root.setAttribute('data-bs-theme', next);
    try {
        localStorage.setItem('theme', next);
    } catch { }
};

// Follow OS preference changes until the user makes an explicit choice.
window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (e) {
    try {
        if (localStorage.getItem('theme')) return;
    } catch { }
    document.documentElement.setAttribute('data-bs-theme', e.matches ? 'dark' : 'light');
});
