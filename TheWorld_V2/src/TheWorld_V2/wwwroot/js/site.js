// site.js
(function () {
    //$(document).ready(function () {
    //    console.log("ready!");

    //    var ele = $("#username");
    //    ele.text("Colin Detwiller");

    //    var main = $("#main");
    //    main.on("mouseenter", function () {
    //        main.css("background-color", "#888");
    //        //main.style["background-color"] = "#888";
    //    });

    //    main.on("mouseleave", function () {
    //        main.css("background-color", "#eee");
    //    });

    //    var menuItems = $("ul.menu li a");
    //    menuItems.on("click", function () {
    //        var me = $(this);
    //        alert(me.text());
    //    });
    //});
    var $button = $("#sidebarToggle");
    var $sidebarAndWrapper = $("#sidebar,#wrapper");

    $button.on("click", function() {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        } else {
            $(this).text("Hide Sidebar");
        }
    });
})();
