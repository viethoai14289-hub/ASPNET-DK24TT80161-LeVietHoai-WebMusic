// WebMusic site.js — slideshow (port từ app.js) + a11y: dot là button, keyboard nav
(function () {
    let slideIndex = 1;

    // exposed cho onclick inline trong TrangChu
    window.plusSlides = function (n) { showSlides(slideIndex += n); };
    window.currentSlide = function (n) { showSlides(slideIndex = n); };

    function showSlides(n) {
        const slides = document.getElementsByClassName('mySlides');
        const dots = document.getElementsByClassName('dot');
        if (slides.length === 0) return;
        if (n > slides.length) slideIndex = 1;
        if (n < 1) slideIndex = slides.length;
        for (let i = 0; i < slides.length; i++) slides[i].style.display = 'none';
        for (let i = 0; i < dots.length; i++) dots[i].classList.remove('active');
        slides[slideIndex - 1].style.display = 'block';
        if (dots[slideIndex - 1]) dots[slideIndex - 1].classList.add('active');
    }

    document.addEventListener('DOMContentLoaded', function () {
        showSlides(slideIndex);

        // keyboard nav cho slideshow khi focus
        const container = document.querySelector('.slideshow-container');
        if (container) {
            container.setAttribute('tabindex', '0');
            container.addEventListener('keydown', function (e) {
                if (e.key === 'ArrowLeft') window.plusSlides(-1);
                if (e.key === 'ArrowRight') window.plusSlides(1);
            });
        }

        // auto-play 5s
        if (document.getElementsByClassName('mySlides').length > 1) {
            setInterval(function () { window.plusSlides(1); }, 5000);
        }
    });
})();