const btn = document.getElementById("nav-collapse")

const nav = document.querySelector(".pkaiser-nav") as HTMLElement
const navItems = document.querySelector(".nav-items") as HTMLAnchorElement

btn?.addEventListener("click", openNav)

function openNav() {
    navItems.style.height = "100%"
    navItems.style.padding = "10px 16px"

    btn?.removeEventListener("click", openNav)
    btn?.addEventListener("click", closeNav)
}

function closeNav() {
    navItems.style.height = "0"
    navItems.style.padding = "0"

    btn?.removeEventListener("click", closeNav)
    btn?.addEventListener("click", openNav)
}