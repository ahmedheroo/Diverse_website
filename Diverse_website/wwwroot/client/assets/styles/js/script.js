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

