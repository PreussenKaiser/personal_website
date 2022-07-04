const spinner = document.getElementById('spinner') as HTMLElement
const text = document.getElementById('loading-text') as HTMLElement
const img = document.querySelector('.profile-image') as HTMLImageElement

setTimeout(() => {
    spinner.style.opacity = '0'
}, 4000)

setTimeout(() => {
    spinner.style.display = 'none'

    text.style.height = 'auto'
    text.style.opacity = '1'
}, 5000)