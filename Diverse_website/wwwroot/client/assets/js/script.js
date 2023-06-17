window.onload = function() {
    var screenWidth = window.innerWidth || document.documentElement.clientWidth;
    var myDiv = document.getElementById('pagesNUM');

    if (screenWidth < 992) {
        myDiv?.classList.remove('text-right');
        myDiv?.classList.add('text-center');
    } else {
        myDiv?.classList.remove('text-center');
        myDiv?.classList.add('text-right');
    }
};

// Slider

function Slider() {
    const carouselSlides = document.querySelectorAll('.slide');
    const btnPrev = document.querySelector('.prev');
    const btnNext = document.querySelector('.next');
    let currentSlide = 0;
  
    const changeSlide = function (slides) {
        carouselSlides.forEach((slide, index) => {
            const offset = index - slides;
            slide.style.transform = `translateX(${100 * offset}%)`;
        });
    };


    const nextSlide = function () {
        currentSlide++;
        if (carouselSlides.length <= currentSlide) {
            currentSlide = 0;
        }
        changeSlide(currentSlide);
    };
  
    const prevSlide = function () {
        currentSlide--;
        if (currentSlide < 0) {
            currentSlide = carouselSlides.length - 1;
        }
        changeSlide(currentSlide);
    };
  
    const startSlider = function () {
        changeSlide(currentSlide); // Set initial slide position
        setInterval(nextSlide, 3000); // Slide every 3 seconds
    };
  
    btnNext.addEventListener('click', nextSlide);
    btnPrev.addEventListener('click', prevSlide);
  
    startSlider();
    carouselSlides[0].classList.add('active');
}
Slider();


function toggleCards() {
    const cards = document.querySelectorAll('.col-md-4');
    const loadMoreButton = document.querySelector('.load-more-maso');
  
    const showAllCards = () => {
      cards.forEach((card, index) => {
        card.style.display = 'block';
      });
      loadMoreButton.textContent = 'Load Less';
    };
  
    const hideExtraCards = () => {
      cards.forEach((card, index) => {
        if (index >= 3) {
          card.style.display = 'none';
        }
      });
      loadMoreButton.textContent = 'Load More';
    };
  
    loadMoreButton.addEventListener('click', function () {
      if (loadMoreButton.textContent === 'Load More') {
        showAllCards();
      } else {
        hideExtraCards();
      }
    });
  
    // Hide initial extra cards
    hideExtraCards();
  }
  
  toggleCards();
  