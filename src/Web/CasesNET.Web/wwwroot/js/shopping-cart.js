"use strict";
$(document).ready(function () {

    $('.shopping-cart-form').submit(async function (e) {
        e.preventDefault();
        const url = $(this).attr('action');
        const antiForgerytoken = $(this.elements['__RequestVerificationToken']).val();
        await $.ajax({
            url,
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "RequestVerificationToken": antiForgerytoken
            },
            error: function (error) {
                if (error.status === 401) {
                    window.location.href = "https://localhost:44319/identity/Account/login"
                }
            },
        })
        const count = await GetCount();
        updateBagQuantity(count);
       
    })

    
})
async function GetCount() {
    const url = 'https://localhost:44319/api/cart/count';
    try {
        const count = await $.get({
            url: url,
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
            }
        });
        return count;
    } catch (e) {
        return 0;
    }

}
$('.removeItem-form').submit(async function (e) {
    e.preventDefault();
    const url = $(this).attr('action');
    const antiForgerytoken = $(this.elements['__RequestVerificationToken']).val();
    await $.ajax({
        url,
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "RequestVerificationToken": antiForgerytoken
        }
    })

    //get total price without the dollar sign
    let currentTotalPrice = $('#totalPrice').text().substring(1);

    const currentItem = $(e.target).parents().get(1);
    const currentItemQuantity = +$(currentItem).children(".cart-item-quantity").text().trim();
    const currentItemPrice = +$(currentItem).children('.cart-item-price').text().trim().substring(1);
    const currentItemTotalPrice = currentItemQuantity * currentItemPrice;   
    currentTotalPrice -= currentItemTotalPrice;
    $('#totalPrice').text("$" + currentTotalPrice.toFixed(2))
    currentItem.remove();
    //updates bad when items are removed
    const count = await GetCount();
    updateBagQuantity(count);
    if (count === 0) {
        location.reload();
    }
});

