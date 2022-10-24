$(document).ready(function () {
    $(".page-link").click(function (e) {
        e.preventDefault();
        let url = this.attributes["href"].value;
        $("#carsList").load(url);
        $(".active").removeClass("active disabled");
        $(this).parent().addClass("active");
        history.pushState(null, null, url);
    });
});