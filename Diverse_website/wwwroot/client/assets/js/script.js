window.onload = function() {
    var screenWidth = window.innerWidth || document.documentElement.clientWidth;
    var myDiv = document.getElementById('pagesNUM');

    if (screenWidth < 992) {
        myDiv.classList.remove('text-right');
        myDiv.classList.add('text-center');
    } else {
        myDiv.classList.remove('text-center');
        myDiv.classList.add('text-right');
    }
};

// Slider

function Slider() {
    const carouselSlides = document.querySelectorAll('.slide');
    const btnPrev = document.querySelector('.prev');
    const btnNext = document.querySelector('.next');
    let currentSlide = 0;
  


    const changeSlide = function (slides) {
        carouselSlides.forEach((slide, index) => (slide.style.transform = `translateX(${100 * (index - slides)}%)`));
    };
    changeSlide(currentSlide);

    btnNext.addEventListener('click', function () {
        currentSlide++; 
        if (carouselSlides.length - 1 < currentSlide) {
            currentSlide = 0;
        };
        changeSlide(currentSlide);
});
    btnPrev.addEventListener('click', function () {
        currentSlide--;
        if (0 >= currentSlide) {
            currentSlide = 0;
        }; 
        changeSlide(currentSlide);
    });
};
Slider();