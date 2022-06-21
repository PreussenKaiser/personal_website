"use strict";
const btn = document.getElementById("nav-collapse");
const nav = document.querySelector(".pkaiser-nav");
const navItems = document.querySelector(".nav-items");
btn === null || btn === void 0 ? void 0 : btn.addEventListener("click", openNav);
function openNav() {
    navItems.style.height = "100%";
    navItems.style.padding = "10px 16px";
    btn === null || btn === void 0 ? void 0 : btn.removeEventListener("click", openNav);
    btn === null || btn === void 0 ? void 0 : btn.addEventListener("click", closeNav);
}
function closeNav() {
    navItems.style.height = "0";
    navItems.style.padding = "0";
    btn === null || btn === void 0 ? void 0 : btn.removeEventListener("click", closeNav);
    btn === null || btn === void 0 ? void 0 : btn.addEventListener("click", openNav);
}
