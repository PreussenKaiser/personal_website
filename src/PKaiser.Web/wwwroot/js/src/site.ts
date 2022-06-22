const btn = document.getElementById("nav-collapse")

const nav = document.querySelector(".pkaiser-nav") as HTMLElement
const navWrapper = document.querySelector(".nav-wrapper") as HTMLElement

btn?.addEventListener("click", openNav)

function openNav() {
    navWrapper.style.height = "100%"
    navWrapper.style.padding = "16px"

    btn?.removeEventListener("click", openNav)
    btn?.addEventListener("click", closeNav)
}

function closeNav() {
    navWrapper.style.height = "0"
    navWrapper.style.padding = "0"

    btn?.removeEventListener("click", closeNav)
    btn?.addEventListener("click", openNav)
}