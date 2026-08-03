// Drives the infinite comic feed: watches a sentinel element and asks Blazor for more comics.
window.comicFeed = {
    observer: null,
    sentinel: null,
    dotnetRef: null,

    init: function (sentinel, dotnetRef) {
        this.dispose();
        this.sentinel = sentinel;
        this.dotnetRef = dotnetRef;

        this.observer = new IntersectionObserver(async (entries) => {
            if (!entries[0].isIntersecting) {
                return;
            }

            // Pause while a batch loads, then re-check so the page keeps filling until the sentinel is off-screen.
            this.observer.unobserve(sentinel);
            try {
                await dotnetRef.invokeMethodAsync('LoadMoreFromJs');
            } catch {
                return;
            }
            if (this.observer && this.sentinel) {
                this.observer.observe(this.sentinel);
            }
        }, { rootMargin: '600px 0px' });

        this.observer.observe(sentinel);
    },

    scrollTop: function () {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    },

    dispose: function () {
        if (this.observer) {
            this.observer.disconnect();
            this.observer = null;
        }
        this.sentinel = null;
        this.dotnetRef = null;
    }
};
