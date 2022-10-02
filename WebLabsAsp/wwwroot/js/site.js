const activePage = window.location.pathname;
const navLinks = document.querySelectorAll('header div a').
forEach(link => {
    if(link.classList.contains('but') && link.attributes.getNamedItem('href').value === activePage)
    {
        link.classList.add('active');
    }
})
