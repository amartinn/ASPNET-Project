"use strict";
$(document).ready(function () {

    $('.shopping-cart-form').submit(async function (e) {
        e.preventDefault();
        const url = $(this).attr('action');
        const antiForgerytoken = $(this.elements['__RequestVerificationToken']).val();
        await $.ajax({
            url,
            method:"POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "RequestVerificationToken": antiForgerytoken
            },
            error: function (error) {
                if (error.status === 401) {
                    window.location.href ="https://localhost:44319/identity/Account/login"
                }
            }
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
    const currentRow = $(e.target).parents().get(1);
    currentRow.remove();
});