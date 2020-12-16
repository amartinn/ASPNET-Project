"use strict";
$(document).ready(function () {
    $(".hamburger ").click(function () {
        $(this).toggleClass("is-active ");
        $('.nav-items-dropdown').toggleClass('d-block show');
    });
});
function updateBagQuantity(count) {
    $('.shopping-bag').attr('data-content', count);
}