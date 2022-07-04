"use strict";
const spinner = document.getElementById('spinner');
const text = document.getElementById('loading-text');
const img = document.querySelector('.profile-image');
setTimeout(() => {
    spinner.style.opacity = '0';
}, 4000);
setTimeout(() => {
    spinner.style.display = 'none';
    text.style.height = 'auto';
    text.style.opacity = '1';
}, 5000);
if (img != null) {
    img.addEventListener('hover', () => {
        img.src = 'Resources/img/pkaiser.jpg';
    });
}
