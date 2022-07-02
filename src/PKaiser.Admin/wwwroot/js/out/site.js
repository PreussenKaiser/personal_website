"use strict";
const btn = document.getElementById("nav-collapse");
const nav = document.querySelector(".pkaiser-nav");
const navWrapper = document.querySelector(".nav-wrapper");
btn === null || btn === void 0 ? void 0 : btn.addEventListener("click", openNav);
function openNav() {
    navWrapper.style.height = "100%";
    navWrapper.style.padding = "16px";
    btn === null || btn === void 0 ? void 0 : btn.removeEventListener("click", openNav);
    btn === null || btn === void 0 ? void 0 : btn.addEventListener("click", closeNav);
}
function closeNav() {
    navWrapper.style.height = "0";
    navWrapper.style.padding = "0";
    btn === null || btn === void 0 ? void 0 : btn.removeEventListener("click", closeNav);
    btn === null || btn === void 0 ? void 0 : btn.addEventListener("click", openNav);
}
